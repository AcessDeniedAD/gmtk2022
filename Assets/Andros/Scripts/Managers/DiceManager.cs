using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceManager : BaseManager
{
    private GameObject _rollingDiceGameObject;
    private readonly GameManager _gameManager;
    private readonly DiceLoader _diceLoader;
    private List<Face> _currentFacesOnDice = new List<Face>();

    public string ShownFaceMaterialName = "mat";

    private List<Face> _faces;
    public DiceManager(PrefabsLoaderManager prefabsLoaderManager, GameManager gameManager)
    {
        _gameManager = gameManager;
        _diceLoader = prefabsLoaderManager.DiceLoader;
        _rollingDiceGameObject =
        _rollingDiceGameObject = _gameManager.InstantiateInGameManager(_diceLoader.RollingDice, Vector3.zero, Quaternion.identity);

        BuildFacesList();
    }

    public void EnableRollingDiceInScene()
    {
        _rollingDiceGameObject.SetActive(true);
    }
    public void DisableRollingDiceInScene()
    {
        _rollingDiceGameObject.SetActive(false);
    }

    public void BuildNewFacesOnDice(int difficultyLevel)
    {
        var mr = _rollingDiceGameObject.GetComponent<MeshRenderer>();
        List<Face> facesScaleDifficultyLevel = _faces.Where(x => x.difficultyLevel <= difficultyLevel).ToList();
        List<Material> newMaterials = new List<Material>();
        for (int i = 0; i < mr.materials.Length; i++)
        {

            var newFaceMaterial = facesScaleDifficultyLevel[Random.Range(0, facesScaleDifficultyLevel.Count() - 1)].material;
            newMaterials.Add(newFaceMaterial);
        }

        if (newMaterials.Count > 0)
        {
            mr.materials = newMaterials.ToArray();
        }

    }

    public void BuildFacesList()
    {
        _faces = new List<Face>()
        {
            new Face(){name = "Red", material = _diceLoader.RedFace, difficultyLevel = 0 , weight = 1, id = 1},
            new Face(){name = "Pink", material = _diceLoader.PinkFace, difficultyLevel = 0 , weight = 1,id = 2},
            new Face(){name = "Purple", material = _diceLoader.PurpleFace,difficultyLevel = 0 , weight = 1, id = 3},
            new Face(){name = "DarkBlue", material = _diceLoader.DarkBlueFace,difficultyLevel = 0 , weight = 1, id = 4},
            new Face(){name = "LightBlue", material = _diceLoader.LightBlueFace,difficultyLevel = 0 , weight = 1, id = 5},
            new Face(){name = "Orange", material = _diceLoader.OrangeFace,difficultyLevel = 0 , weight = 1, id = 6},
            new Face(){name = "White", material = _diceLoader.WhiteFace,difficultyLevel = 0 , weight = 1, id = 7},
        };
    }

    public void RollDice(float time)
    {
        _gameManager.StartCoroutine(RollDiceCoroutine(time));
    }
    public IEnumerator RollDiceCoroutine(float time)
    {
       
        var facesDistance = new Dictionary<string, float>();
        var canCheckDot = true;
        var timer = 0f;
        var initialTime = time;
        var timeToChangeRotation = Random.Range(initialTime / 5, initialTime / 2);
        Vector3 v3 = new Vector3(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f));
        var initalSpeed = Random.Range(0.5f, 4);
        var closestDirection = new KeyValuePair<string, float>();
        Vector3 closestDir = Vector3.zero;
        Quaternion initialRotation = Quaternion.identity;
        float interpolator = 0;
        while (time >= 0)
        {
            time -= Time.deltaTime * Time.timeScale;
            timer += Time.deltaTime * Time.timeScale;

            if (timer > timeToChangeRotation)
            {
                v3 += new Vector3(Random.Range(-50.0f, 50.0f), Random.Range(-50.0f, 50.0f), Random.Range(-50.0f, 50.0f));
                //v3 = new Vector3(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f));
                timer = 0.0f;
                timeToChangeRotation = Random.Range(initialTime / 5, initialTime / 2); ;
            }
            if(time > initialTime / 10)
            {
                _rollingDiceGameObject.transform.Rotate(v3 * Time.deltaTime * Time.timeScale * time * initalSpeed);
            }
            else
            {
               
                var tr = _rollingDiceGameObject.transform;
                var cameraDirection = (Camera.main.transform.position - tr.position).normalized;
                if (canCheckDot)
                {
                  
                    facesDistance.Add("forward", Vector3.Dot(tr.forward, cameraDirection));
                    facesDistance.Add("-forward", Vector3.Dot(-tr.forward, cameraDirection));

                    facesDistance.Add("right", Vector3.Dot(tr.right, cameraDirection));
                    facesDistance.Add("-right", Vector3.Dot(-tr.right, cameraDirection));

                    facesDistance.Add("up", Vector3.Dot(tr.up, cameraDirection));
                    facesDistance.Add("-up", Vector3.Dot(-tr.up, cameraDirection));

                    closestDirection = facesDistance.OrderByDescending(f => f.Value).First();
                    
                    canCheckDot = false;

                    switch (closestDirection.Key)
                    {
                        case "forward":
                            closestDir = tr.forward;
                            SelectMaterialByIndex(3);
                            break;

                        case "-forward":
                            closestDir = -tr.forward;
                            SelectMaterialByIndex(6);
                            break;

                        case "right":
                            closestDir = tr.right;
                            SelectMaterialByIndex(1);
                            break;

                        case "-right":
                            closestDir = -tr.right;
                            SelectMaterialByIndex(5);
                            break;

                        case "up":
                            closestDir = tr.up;
                            SelectMaterialByIndex(4);
                            break;

                        case "-up":
                            closestDir = -tr.up;
                            SelectMaterialByIndex(0);
                            break;

                    }
                    initialRotation = tr.rotation;
                }
                Quaternion qto = new Quaternion();
                qto.SetFromToRotation(closestDir, cameraDirection);
                interpolator += Time.deltaTime;
                _rollingDiceGameObject.transform.rotation = Quaternion.Lerp(initialRotation, qto * initialRotation, interpolator / (initialTime / 10) );
            }

            yield return 0;
        }
    }

    private void SelectMaterialByIndex(int i)
    {
        ShownFaceMaterialName = _rollingDiceGameObject.GetComponent<MeshRenderer>().sharedMaterials[i].name;
        Debug.Log("SelectedMaterial is " + ShownFaceMaterialName);
    }

    public string GetDiceFace()
    {
        var face = _faces.Where(x => x.material.name == ShownFaceMaterialName).Single();
        return face.name;
    }
}

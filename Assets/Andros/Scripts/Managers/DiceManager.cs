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
            new Face(){name = "RedFace", material = _diceLoader.RedFace, difficultyLevel = 0 , weight = 1, id = 1},
            new Face(){name = "PinkFace", material = _diceLoader.PinkFace, difficultyLevel = 0 , weight = 1,id = 2},
            new Face(){name = "PurpleFace", material = _diceLoader.PurpleFace,difficultyLevel = 0 , weight = 1, id = 3},
            new Face(){name = "DarkBlueFace", material = _diceLoader.DarkBlueFace,difficultyLevel = 0 , weight = 1, id = 4},
            new Face(){name = "LightBlueFace", material = _diceLoader.LightBlueFace,difficultyLevel = 0 , weight = 1, id = 5},
            new Face(){name = "OrangeFace", material = _diceLoader.OrangeFace,difficultyLevel = 0 , weight = 1, id = 6},
            new Face(){name = "WhiteFace", material = _diceLoader.WhiteFace,difficultyLevel = 0 , weight = 1, id = 7},
        };
    }

    public void RollDice(float time)
    {
        _gameManager.StartCoroutine(RollDiceCoroutine(time));
    }
    public IEnumerator RollDiceCoroutine(float time)
    {
        var timer = 0f;
        var timeToChangeRotation = Random.Range(time/10, time/2);
        Vector3 v3 = new Vector3(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f));
        var initalSpeed = Random.Range(0.5f, 4);
        while (time >= 0)
        {
            time -= Time.deltaTime * Time.timeScale;
            timer += Time.deltaTime * Time.timeScale;

            if (timer > timeToChangeRotation)
            {
                v3 += new Vector3(Random.Range(-50.0f, 50.0f), Random.Range(-50.0f, 50.0f), Random.Range(-50.0f, 50.0f));
                //v3 = new Vector3(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f));
                timer = 0.0f;
                timeToChangeRotation = Random.Range(time / 10, time / 2); ;
            }
            if(timer> time / 10)
            {
                _rollingDiceGameObject.transform.Rotate(v3 * Time.deltaTime * Time.timeScale * time * initalSpeed);
            }
            else
            {
                _rollingDiceGameObject.transform.Rotate(v3 * Time.deltaTime * Time.timeScale * time * initalSpeed);
            }
            

            yield return 0;
        }

    }
}

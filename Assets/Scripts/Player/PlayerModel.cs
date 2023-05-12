using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    protected enum CarModel { Car5A, Car5B, Car7A }
    protected CarModel model = CarModel.Car5B;

    [SerializeField] protected List<Transform> models;

    protected void Reset()
    {
        this.LoadModel();
    }

    protected void Awake()
    {
        this.LoadModel();
    }

    protected void Update()
    {
        this.ChangeModel();
    }

    protected virtual void LoadModel()
    {
        if (this.models.Count > 0) return;

        foreach (Transform model in transform.Find("ModelsHolder"))
        {
            this.models.Add(model);
        }
    }

    protected virtual void ChangeModel()
    {
        if (InputManager.Instance.Model_l) model = CarModel.Car5A;
        else if (InputManager.Instance.Model_2) model = CarModel.Car5B;
        else if (InputManager.Instance.Model_3) model = CarModel.Car7A;

        for (int i = 0; i < models.Count; i++)
        {
            if (i == (int) model) models[i].gameObject.SetActive(true);
            else models[i].gameObject.SetActive(false);
        }
    }
}

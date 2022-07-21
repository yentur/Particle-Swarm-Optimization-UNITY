using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class inst2 : MonoBehaviour
{
    [SerializeField] GameObject partical;
    [SerializeField] GameObject hedef;
    Int16 number_partical = 150;
    GameObject[] particals = new GameObject[150];

    float[,] best_matrix = new float[150, 3];
    float[] best_matrix_value = new float[150];
    float[] values = new float[150];
    float[,] values_ = new float[150, 3];

    float w = 0.95f;
    float c1 = 1.494f;
    float c2 = 1.494f;
    float delta_t = 0.001f;
    float[] gbest = new float[3];
    float val_gbest = 9999f;
    float value = 0;
    Vector3 hedef_last = new Vector3();





    void Start()
    {

        for (int i = 0; i < number_partical; i++)
        {
            GameObject partical_ = Instantiate(partical);
            partical_.transform.position = new Vector3(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(1f, 9f), UnityEngine.Random.Range(-4f, 4f));
            particals[i] = partical_;
            
            particals[i].GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(0f, 20f), UnityEngine.Random.Range(0f, 20f), UnityEngine.Random.Range(0f, 20f));
            particals[i].GetComponent<Rigidbody>().mass = UnityEngine.Random.Range(0f, 20f);
        }

        for (int i = 0; i < number_partical; i++)
        {
            best_matrix[i, 0] = 0.0f;
            best_matrix[i, 1] = 0.0f;
            best_matrix[i, 2] = 0.0f;
            best_matrix_value[i] = 9999f;
            values[i] = 9999f;


        }


    }


    void Update()
    {

        if (w > 0.4) w = w - 0.001f;
        particalUpdate();
        if (hedef.transform.position != hedef_last)
        {
            for (int i = 0; i < number_partical; i++)
            {

                val_gbest = 9999f;
                value = 0;
                best_matrix_value[i] = 9999f;
                //particals[i].transform.position = new Vector3(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(1f, 9f), UnityEngine.Random.Range(-4f, 4f));
                values[i] = 9999f;
                particals[i].GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-20, 20f), UnityEngine.Random.Range(-20, 20f));



            }
        }
        hedef_last = hedef.transform.position;

    }

    private void FixedUpdate()
    {
        

    }



    private void particalUpdate()
    {
        for (int i = 0; i < number_partical; i++)
        {


            float x_v = particals[i].GetComponent<Rigidbody>().velocity.x;
            float y_v = particals[i].GetComponent<Rigidbody>().velocity.y;
            float z_v = particals[i].GetComponent<Rigidbody>().velocity.z;

            float x = (w * x_v) + (c1 * UnityEngine.Random.Range(0f, 1f) * (best_matrix[i, 0] - particals[i].transform.position.x)) + (c2 * UnityEngine.Random.Range(0f, 1f) * (gbest[0] - particals[i].transform.position.x));

            float y = (w * y_v) + (c1 * UnityEngine.Random.Range(0f, 1f) * (best_matrix[i, 1] - particals[i].transform.position.y)) + (c2 * UnityEngine.Random.Range(0f, 1f) * (gbest[1] - particals[i].transform.position.y));

            float z = (w * z_v) + (c1 * UnityEngine.Random.Range(0f, 1f) * (best_matrix[i, 2] - particals[i].transform.position.z)) + (c2 * UnityEngine.Random.Range(0f, 1f) * (gbest[2] - particals[i].transform.position.z));


            particals[i].GetComponent<Rigidbody>().velocity = new Vector3(x, y, z);

            particals[i].transform.position = new Vector3(particals[i].transform.position.x + x * delta_t, particals[i].transform.position.y + y * delta_t, particals[i].transform.position.z + z * delta_t);

            value = amacfonksiyonu(particals[i].transform.position.x, particals[i].transform.position.y, particals[i].transform.position.z);
            
            
            values[i] = value;
            values_[i, 0] = particals[i].transform.position.x;
            values_[i, 1] = particals[i].transform.position.y;
            values_[i, 2] = particals[i].transform.position.z;


            if (best_matrix_value[i] > value)
            {
                best_matrix_value[i] = value;
                best_matrix[i, 0] = particals[i].transform.position.x;
                best_matrix[i, 1] = particals[i].transform.position.y;
                best_matrix[i, 2] = particals[i].transform.position.z;

            }

            if (val_gbest > values.Min())
            {
                int minIndex = Array.IndexOf(values, values.Min());
                gbest[0] = values_[minIndex, 0];
                gbest[1] = values_[minIndex, 1];
                gbest[2] = values_[minIndex, 2];
                val_gbest = values.Min();
            }



        }
    }


    private float amacfonksiyonu(float x, float y, float z)
    {

        return (x- hedef.transform.position.x) * (x - hedef.transform.position.x) +(y-hedef.transform.position.y)* (y - hedef.transform.position.y) + (z - hedef.transform.position.z) * (z - hedef.transform.position.z);
    }
}

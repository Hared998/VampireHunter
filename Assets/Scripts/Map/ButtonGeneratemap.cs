using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonGeneratemap : MonoBehaviour
{

    public InputField x;
    public InputField y;
    public InputField mapsizex;
    public InputField mapsizey;
    public Dropdown biome;
    // Start is called before the first frame update

    public void GenerateMap()
    {
        GameObject generator = GameObject.FindGameObjectsWithTag("Generator")[0];
        generator.GetComponent<DungeonGenerator>().Generate(int.Parse(x.text), int.Parse(y.text), int.Parse(mapsizex.text), int.Parse(mapsizey.text), biome.options[biome.value].text);

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

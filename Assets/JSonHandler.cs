using UnityEngine;
using System.Collections;
//using LitJson;
using JsonFx.Json;
using System.IO;

public class JSonHandler : MonoBehaviour {

    public class Student
    {
        public Student() { }
        public Student(string n, int a, int[] c) { }
        public string name{get;set;}
        public float age{get;set;}
        public int[] count{get;set;}

    }


    Student st1 = new Student();
    Student st2 = new Student();
	void Start () {
        st1.age = 3;
        st1.count = new int[] { 2, 5, 35, 234, 75, 6, 234, 7, 2232323, -6 };
        st1.name = "Ivan";
        
        st2.age = 4;
        st2.count = new int[] { 2, 5, 35, 234, 75, 6, 234, 7, 2232323, -6 };
        st2.name = "Serega";

	}
	
    string content = "hello!!!!!!!!!!!!!";

    void OnGUI() {
        content = GUI.TextArea(new Rect(100, 100, 400, 100), content);

       /* if (GUI.Button(new Rect(330, 60, 60, 30), "Little jasson"))
        {
            Debug.Log("write jsson");
          
           

            TextWriter wr = new StreamWriter("students.txt");
            JsonWriter jsonwr = new JsonWriter(wr);
            jsonwr.IndentValue = 0;
            JsonMapper.ToJson(st1, jsonwr);
            jsonwr.WriteObjectStart();
           
            JsonMapper.ToJson(st2, jsonwr);
            wr.Close();

        }*/

        if (GUI.Button(new Rect(380, 60, 60, 30), "FX jasson"))
        {
            /*Классы короткая запись*/
            string json = JsonFx.Json.JsonWriter.Serialize(st1);
           // Debug.Log(json);
            Student st3 = JsonFx.Json.JsonReader.Deserialize<Student>(json);
            //Debug.Log("age " + st3.age + " count " + st3.count[5] + " name " + st3.name);





            /*массив*/

            JsonWriterSettings settw = new JsonWriterSettings();
            settw.TypeHintName = "__type";
            JsonReaderSettings settr = new JsonReaderSettings();
            settr.TypeHintName = "__type";



            /*пример чтоб разобраться, но либа работает только с классами нормально. */
            System.Text.StringBuilder builder=new System.Text.StringBuilder();
            JsonWriter wr = new JsonWriter(builder,settw);
                Student[] arr = new Student[3];
                arr[0] = st1;
                arr[1] = st2;
                arr[2] = st3;
                wr.Write(arr);
                System.IO.File.WriteAllText("testJSON.txt", builder.ToString());  
           
            string jsonText = System.IO.File.ReadAllText("testJSON.txt");
            //Debug.Log(""+jsonText);
            Student[] tempSt = JsonReader.Deserialize<Student[]>(jsonText);
            content = "";
                foreach( var s in tempSt)
                    content +="" + s.name+System.Environment.NewLine;
            
        }
    }
}

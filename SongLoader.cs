using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class SongLoader : MonoBehaviour
{
    public string txtDocumentName;
    public static SongLoader instance;
    //Variables for LoadChart func
    string[] collectedData = new string[4];
    int dataState, NoteID = 0;

    void Start()
    {
        instance = this;
        Directory.CreateDirectory(Application.streamingAssetsPath + "/SongData/");
        //CreateTextFile();
    }

    public void CreateTextFile(){
        txtDocumentName = Application.streamingAssetsPath + "/SongData/" + "KOU" + ".ubd";

        if(!File.Exists(txtDocumentName)){
            File.WriteAllText(txtDocumentName, "SONG TITLE \r\n");
        }
    }

    public void WriteChart(){
        foreach(Note noteData in SongManager.instance.SN){
            File.AppendAllText(
                txtDocumentName,
                $"\r\n{ParseBToS(noteData.isEnemy)}_{ParseBToS(noteData.isPlayer)}_{noteData.ArrID}_{noteData.time};");
        }
    }

    public void LoadChart(){
        List<string> lines = File.ReadAllLines(txtDocumentName).ToList();
        foreach(string line in lines){
            int lineLen = line.Length;
            for(int i = 0; i < lineLen; i++){
                if(line[i] != '_' || line[i] != ';'){
                    collectedData[dataState] += line[i];
                } else {
                    dataState += 1;
                }
            }
            SongManager.instance.SN.Add(
                new Note{
                isEnemy = ParseBool(collectedData[0]),
                isPlayer = ParseBool(collectedData[1]),
                ArrID = int.Parse(collectedData[2]),
                time = float.Parse(collectedData[3])
                }
            );
            dataState = 0;
            NoteID += 1;
        }
    }

    bool ParseBool(string strToParse){
        if(strToParse == "1"){
            return true;
        } else {
            return false;
        }
    }

    string ParseBToS(bool boolToParse){
        if(boolToParse == true){
            return("1");
        } else {
            return("0");
        }
    }
}
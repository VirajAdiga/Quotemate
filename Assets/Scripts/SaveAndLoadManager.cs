/*
Save and load manager script handles serialization of data to a file while the application is made to quit.
Loads the content of the file saved when the app is relaunched. This contains a dictionary which has id
and respective quotes of the single quote objects which was previously edited and saved by the user. This dictionary 
has public instance. Only one instance of this class can be created as this uses singleton.
*/

using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveAndLoadManager : MonoBehaviour
{
    //List of Single quote objects to be written to while editing
    private List<SingleQuoteSave> mSingleQuotesToBeWrittenTo=new List<SingleQuoteSave>();

    //Array of SingleQuote objects read from the saved file
    private SingleQuoteSave[] mSingleQuotesReadFromTheFile=null;

    //Dictionary to check whether the prevoius content of particular SingleQuote object is edited
    public Dictionary<int,string> mQuoteAndId=new Dictionary<int, string>();

    //Path to save the file
    private string mDirectoryOfSavedFile="/SaveData/";
    private string mNameOfTheFile="quotes.txt";


    //Singleton
    private SaveAndLoadManager(){

    }
    private static SaveAndLoadManager sSaveAndLoadMaNgInstance;
    public static SaveAndLoadManager saveAndLoadManager{
        get{
            return sSaveAndLoadMaNgInstance;
        }
    }


    private void Awake(){
        sSaveAndLoadMaNgInstance=this;
    }


    private void Start(){
        LoadDataOnAwake();
    }


    //Method to write data to single quote object if edit takes place
    public void SaveTheDataOfParticularID(int inIdOfTheQuote,string inUserEditedQuote){

        for(int index=0;index<mSingleQuotesToBeWrittenTo.Count;index++){
            if(mSingleQuotesToBeWrittenTo[index].Id==inIdOfTheQuote){
                mSingleQuotesToBeWrittenTo[index].Quote=inUserEditedQuote;
                return;
            }
        }

        SingleQuoteSave newObject=new SingleQuoteSave();
        newObject.Id=inIdOfTheQuote;
        newObject.Quote=inUserEditedQuote;
        mSingleQuotesToBeWrittenTo.Add(newObject);
    }


    //Method to save the data to file while quitting
    private void SaveDataOnQuit(){
        string persistentDataPath=Application.persistentDataPath+mDirectoryOfSavedFile;

        if(!Directory.Exists(persistentDataPath))
        Directory.CreateDirectory(persistentDataPath);
        string textToBeWrittenToFile=JsonConvert.SerializeObject(mSingleQuotesToBeWrittenTo);
        File.WriteAllText(persistentDataPath+mNameOfTheFile,textToBeWrittenToFile);
    }

    //Method to load data from saved file while launching the app
    private void LoadDataOnAwake(){
        string persistentDataPath=Application.persistentDataPath+mDirectoryOfSavedFile;
        if(Directory.Exists(persistentDataPath)){
            string savedDataPath=Application.persistentDataPath+mDirectoryOfSavedFile+mNameOfTheFile;
            if(File.Exists(savedDataPath)){
                string jsonData=File.ReadAllText(savedDataPath);
                mSingleQuotesReadFromTheFile=JsonConvert.DeserializeObject<SingleQuoteSave[]>(jsonData);
            }
        }
        if(mSingleQuotesReadFromTheFile!=null){
            for(int index=0;index<mSingleQuotesReadFromTheFile.Length;index++){
                mQuoteAndId.Add(mSingleQuotesReadFromTheFile[index].Id,mSingleQuotesReadFromTheFile[index].Quote);
            }

            for(int index=0;index<mSingleQuotesReadFromTheFile.Length;index++){
                SingleQuoteSave newObject=new SingleQuoteSave();
                newObject.Id=mSingleQuotesReadFromTheFile[index].Id;
                newObject.Quote=mSingleQuotesReadFromTheFile[index].Quote;
                mSingleQuotesToBeWrittenTo.Add(newObject);
            }
        }
    }

    private void OnApplicationQuit() {
        SaveDataOnQuit();
    }
}

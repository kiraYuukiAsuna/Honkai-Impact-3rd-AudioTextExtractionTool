using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutoExtractCharacterAudioAndTextTool;

public class ExportStep
{
    public ExportStep(string resultSavePath)
    {
        m_ResultSavePath = resultSavePath;
    }

    public bool CopyTargetTextJsonFile(string dataExtractionPath, string textAssetExtractionPath)
    {
        /*if (!Path.Exists(dataExtractionPath))
        {
            Console.WriteLine("Data Extraction Path {0} do not exit!", dataExtractionPath);
            return false;
        }

        if (!Path.Exists(textAssetExtractionPath))
        {
            Console.WriteLine("Text Asset Extraction Path {0} do not exit!", textAssetExtractionPath);
            return false;
        }

        if (Path.Exists(m_ResultSavePath))
        {
            Directory.GetFiles(m_ResultSavePath).ToList().ForEach(File.Delete);
            Directory.Delete(m_ResultSavePath,true);
        }
        Directory.CreateDirectory(m_ResultSavePath);

        CopyExtractionFile fileList = new CopyExtractionFile();
        fileList.DataFilePath = new List<string>(){
            "DialogData.json",
            "RandomDialogData_cn.json",
            "RandomDialogData_en.json",
            "TextMap.json",
            "TextMap_cn.json",
            "TextMap_en.json"
        };
        fileList.AssetFilePath = new List<string>();

        foreach (var filePath in Directory.GetFiles(textAssetExtractionPath))
        {
            JObject obj=new JObject();
            try
            {
                obj = JObject.Parse(File.ReadAllText(filePath));
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine(e.Message);
            }

            if (obj.ContainsKey("Tracks"))
            {
                fileList.AssetFilePath.Add(Path.GetFileName(filePath));
            }
        }*/

        m_JsonResultPath = Path.Combine(m_ResultSavePath, "JsonResult");
        m_TextAssetPath = Path.Combine(m_ResultSavePath, "TextAsset");
        m_TextMapPath = Path.Combine(m_ResultSavePath, "TextMap");
        /*
        Directory.CreateDirectory(m_JsonResultPath);
        Directory.CreateDirectory(m_TextAssetPath);
        Directory.CreateDirectory(m_TextMapPath);
        */

        /*var jsonResultTextMap = JsonConvert.SerializeObject(fileList.DataFilePath);
        var jsonResultTextAsset = JsonConvert.SerializeObject(fileList.AssetFilePath);

        var result = JsonConvert.SerializeObject(fileList,Formatting.Indented);

        File.WriteAllText(Path.Combine(m_JsonResultPath,"TargetFile.json"),result);

        foreach (var dataFile in fileList.DataFilePath)
        {
            File.Copy(Path.Combine(dataExtractionPath,dataFile),Path.Combine(m_TextMapPath,dataFile));
        }

        foreach (var assetPath in fileList.AssetFilePath)
        {
            File.Copy(Path.Combine(textAssetExtractionPath,assetPath),Path.Combine(m_TextAssetPath,assetPath));
        }
        Console.WriteLine("Copy Ok");*/
        return true;
    }

    public bool DialogDataExtract()
    {
        var obj = JObject.Parse(File.ReadAllText(Path.Combine(m_TextMapPath, "DialogData.json")));

        ExportJsonDialogDataList exportJsonDialogDataList = new ExportJsonDialogDataList();
        exportJsonDialogDataList.List = new List<ExportJsonDialogData>();
        foreach (var keyValue in obj)
        {
            ExportJsonDialogData data = new ExportJsonDialogData();
            data.ChatContent = new List<string>();
            var val = (JObject) keyValue.Value;
            if (val.ContainsKey("AvatarID"))
            {
                data.AvatarId = val["AvatarID"].Value<string>();
            }

            if (val.ContainsKey("AudioID"))
            {
                data.AudioId = val["AudioID"].Value<string>();
            }

            if (val.TryGetValue("Content", out var content))
            {
                var contentObj = content.Value<JArray>();

                foreach (var chatContent in contentObj)
                {
                    var chatContentObj = chatContent.Value<JObject>();
                    if (chatContentObj.ContainsKey("ChatContent"))
                    {
                        data.ChatContent.Add(chatContentObj["ChatContent"].Value<string>());
                    }
                }
            }

            exportJsonDialogDataList.List.Add(data);
        }

        var str = JsonConvert.SerializeObject(exportJsonDialogDataList, Formatting.Indented);
        File.WriteAllText(Path.Combine(m_JsonResultPath, "DialogDataExtract.json"), str);
        return true;
    }

    public bool RandomDialogDataExtract()
    {
        var obj = JObject.Parse(File.ReadAllText(Path.Combine(m_TextMapPath, "RandomDialogData_cn.json")));

        ExportJsonRandomDialogDataList exportJsonRandomDialogDataList = new ExportJsonRandomDialogDataList();
        exportJsonRandomDialogDataList.List = new List<ExportJsonRandomDialogData>();

        foreach (var keyValue in obj)
        {
            ExportJsonRandomDialogData data = new ExportJsonRandomDialogData();
            data.ChatContent = new List<string>();
            var val = (JObject) (keyValue.Value);
            if (val.ContainsKey("AvatarName"))
            {
                data.AvatarName = val["AvatarName"].Value<string>();
            }

            if (val.ContainsKey("AvatarId"))
            {
                data.AvatarId = val["AvatarId"].Value<string>();
            }

            if (val.ContainsKey("AudioId"))
            {
                data.AudioId = val["AudioId"].Value<string>();
            }

            if (val.TryGetValue("Content", out var content))
            {
                var contentObj = content.Value<JArray>();

                foreach (var chatContent in contentObj)
                {
                    var chatContentObj = chatContent.Value<JObject>();
                    if (chatContentObj.ContainsKey("ChatContent"))
                    {
                        data.ChatContent.Add(chatContentObj["ChatContent"].Value<string>());
                    }
                }
            }

            exportJsonRandomDialogDataList.List.Add(data);
        }

        var str = JsonConvert.SerializeObject(exportJsonRandomDialogDataList, Formatting.Indented);
        File.WriteAllText(Path.Combine(m_JsonResultPath, "RandomDialogDataExtract.json"), str);
        return true;
    }

    public bool PlotLineDataAkaTextMapDataExtract()
    {
        Dictionary<string, string> textMap = new Dictionary<string, string>();

        var textMapJson = JObject.Parse(File.ReadAllText(Path.Combine(m_TextMapPath, "TextMap.json")));
        foreach (var pair in textMapJson)
        {
            var key = pair.Key;
            var valObj = pair.Value.Value<JObject>();
            if (valObj.ContainsKey("Text") && !textMap.ContainsKey(key))
            {
                var value = valObj["Text"].Value<string>();
                textMap.Add(key, value);
            }
        }

        var textMapJsonCN = JObject.Parse(File.ReadAllText(Path.Combine(m_TextMapPath, "TextMap_cn.json")));
        foreach (var pair in textMapJsonCN)
        {
            var key = pair.Key;
            var valObj = pair.Value.Value<JObject>();
            if (valObj.ContainsKey("Text") && !textMap.ContainsKey(key))
            {
                var value = valObj["Text"].Value<string>();
                textMap.Add(key, value);
            }
        }

        ExportJsonPlotlineDataList exportJsonPlotlineDataList = new ExportJsonPlotlineDataList();
        exportJsonPlotlineDataList.List = new List<ExportJsonPlotlineData>();

        var textAssetFiles = Directory.GetFiles(m_TextAssetPath);
        foreach (var textAssetFile in textAssetFiles)
        {
            ExportJsonPlotlineDataList exportJsonPlotlineDataListSingleFile = new ExportJsonPlotlineDataList();
            exportJsonPlotlineDataListSingleFile.List = new List<ExportJsonPlotlineData>();
            var textAssetObj = JObject.Parse(File.ReadAllText(textAssetFile));
            if (textAssetObj.TryGetValue("Tracks", out var clipsListObj))
            {
                foreach (var clips in clipsListObj)
                {
                    if (clips.Type == JTokenType.Object)
                    {
                        if (!clips.Value<JObject>().ContainsKey("Clips"))
                        {
                            continue;
                        }

                        var clipArrayObj = clips["Clips"];
                        foreach (var clip in clipArrayObj)
                        {
                            var clipObj = clip.Value<JObject>();
                            if (clipObj.ContainsKey("ActorNameID"))
                            {
                                var actorNameID = clipObj["ActorNameID"].Value<string>();
                                if (clipObj.ContainsKey("ContentID"))
                                {
                                    var contentID = clipObj["ContentID"].Value<string>();
                                    if (clipObj.ContainsKey("DialogID"))
                                    {
                                        var dialogID = clipObj["DialogID"].Value<string>();
                                        
                                        ExportJsonPlotlineData exportJsonPlotlineData = new ExportJsonPlotlineData();
                                        exportJsonPlotlineData.ActorNameID = actorNameID;
                                        exportJsonPlotlineData.ContentID = contentID;
                                        exportJsonPlotlineData.DialogID = dialogID;
                                        exportJsonPlotlineDataListSingleFile.List.Add(exportJsonPlotlineData);
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var clips in clipsListObj)
                {
                    if (clips.Type == JTokenType.Object)
                    {
                        if (!clips.Value<JObject>().ContainsKey("Clips"))
                        {
                            continue;
                        }

                        var clipArrayObj = clips["Clips"];
                        foreach (var clip in clipArrayObj)
                        {
                            var clipObj = clip.Value<JObject>();
                            if (clipObj.ContainsKey("DialogueAudioName"))
                            {
                                var dialogAudioName = clipObj["DialogueAudioName"].Value<string>();
                                if (clipObj.ContainsKey("DialogID"))
                                {
                                    for (int i = 0; i < exportJsonPlotlineDataListSingleFile.List.Count; i++)
                                    {
                                        var dialogID = clipObj["DialogID"].Value<string>();
                                        
                                         var plotlineData = exportJsonPlotlineDataListSingleFile.List[i];
                                         if (plotlineData.DialogID == dialogID)
                                         {
                                             ExportJsonPlotlineData exportJsonPlotlineData =
                                                 new ExportJsonPlotlineData();

                                             exportJsonPlotlineData.ActorNameID = plotlineData.ActorNameID;
                                             exportJsonPlotlineData.ContentID = plotlineData.ContentID;
                                             exportJsonPlotlineData.DialogID = plotlineData.DialogID;
                                             exportJsonPlotlineData.DialogueAudioName = dialogAudioName;
                                             exportJsonPlotlineData.ChatContent = plotlineData.DialogID;
                                             exportJsonPlotlineDataListSingleFile.List[i] = exportJsonPlotlineData;
                                             break;
                                         }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            foreach (var data in exportJsonPlotlineDataListSingleFile.List)
            {
                exportJsonPlotlineDataList.List.Add(data);
            }
        }


        Int32 numFailed = 0;
        Int32 numSuccess = 0;
        for (int i = 0; i < exportJsonPlotlineDataList.List.Count; i++)
        {
            var data = exportJsonPlotlineDataList.List[i];
            if (string.IsNullOrEmpty(data.ContentID))
            {
                numFailed++;
                continue;
            }

            var hash = HashHelper.GetHashCodeExcel(data.ContentID);
            var hashString = Convert.ToString(hash);
            if (textMap.TryGetValue(hashString, out var value))
            {
                data.ChatContent = value;
                exportJsonPlotlineDataList.List[i] = data;
                numSuccess++;
            }

            if (string.IsNullOrEmpty(data.DialogueAudioName) || string.IsNullOrEmpty(data.ChatContent))
            {
                numFailed++;
            }
        }
        Console.WriteLine("Failed number: {0}",numFailed);
        Console.WriteLine("Success number: {0}",numSuccess);

        var str = JsonConvert.SerializeObject(exportJsonPlotlineDataList, Formatting.Indented);
        File.WriteAllText(Path.Combine(m_JsonResultPath, "PlotLineDataAkaTextMapDataExtract.json"), str);
        return true;
    }

    public bool AudioExtract(string audioExtractionPath)
    {
        var DialogMetaInfo = File.ReadAllText(Path.Combine(m_JsonResultPath, "DialogDataExtract.json"));
        var RandomDialogMetaInfo = File.ReadAllText(Path.Combine(m_JsonResultPath, "RandomDialogDataExtract.json"));
        var PlotlineMetaInfo = File.ReadAllText(Path.Combine(m_JsonResultPath, "PlotLineDataAkaTextMapDataExtract.json"));

        var dialogDataList = JsonConvert.DeserializeObject<ExportJsonDialogDataList>(DialogMetaInfo);
        var randomDialogDataList = JsonConvert.DeserializeObject<ExportJsonRandomDialogDataList>(RandomDialogMetaInfo);
        var plotlineDataList = JsonConvert.DeserializeObject<ExportJsonPlotlineDataList>(PlotlineMetaInfo);

        Dictionary<string, string> audioNameToPathMap=new Dictionary<string, string>();
        var audioFiles = Directory.GetFiles(audioExtractionPath);
        foreach (var audioFile in audioFiles)
        {
            if (Path.GetExtension(audioFile) == ".wav")
            {
                var audioHashName = Path.GetFileNameWithoutExtension(audioFile);
                audioNameToPathMap.Add(audioHashName,audioFile);
            }
        }

        m_AudioResultPath = Path.Combine(m_ResultSavePath, "AudioResult");
        
        ExportJsonAudioTextMapList finalList = new ExportJsonAudioTextMapList();
        finalList.List = new List<ExportJsonAudioTextMap>();
        List<string> auPrefixName = new List<string>()
        {
            "Chinese_DLC\\Story\\",
            "Chinese_Event\\Story\\",
            "Chinese_Ex\\DLC\\",
            "Chinese_Ex\\Dorm\\",
            "Chinese_Ex\\Event\\",
            "Chinese_Ex\\Main\\",
            "Chinese_Ex\\MoonVerMemory\\",
            "Chinese_Ex\\Rogue\\",
            "Chinese_Main\\Story\\"
        };

        Int32 FailedNumber1 = 0;
        Int32 SuccessNumber1 = 0;
        foreach (var dialog in dialogDataList.List)
        {
            if (string.IsNullOrEmpty(dialog.AudioId))
            {
                continue;
            }

            bool bFind = false;
            foreach (var auPrefix in auPrefixName)
            {
                string fullQName = auPrefix + dialog.AudioId+".wem";
                fullQName = fullQName.ToLower();
                var hash = HashHelper.GetFnv1_64Hash(fullQName);
                var hashString = hash.ToString("x");

                if (audioNameToPathMap.ContainsKey(hashString))
                {
                    ExportJsonAudioTextMap exportJsonAudioTextMap = new ExportJsonAudioTextMap();
                    exportJsonAudioTextMap.ChatContent = dialog.ChatContent.Count>0?dialog.ChatContent[0]:null;
                    exportJsonAudioTextMap.ActorName = dialog.AvatarId;
                    exportJsonAudioTextMap.AudioHashString = hashString;
                    bFind = true;
                    SuccessNumber1++;
                    break;
                }
            }

            if (!bFind)
            {
                FailedNumber1++;
            }
        }        
        Console.WriteLine("Failed number1: {0}",FailedNumber1);
        Console.WriteLine("Success number1: {0}",SuccessNumber1);

        Int32 FailedNumber2 = 0;
        Int32 SuccessNumber2 = 0;
        foreach (var randomDialog in randomDialogDataList.List)
        {
            if (string.IsNullOrEmpty(randomDialog.AudioId))
            {
                continue;
            }
            bool bFind = false;
            foreach (var auPrefix in auPrefixName)
            {
                string fullQName = auPrefix + randomDialog.AudioId +".wem";
                fullQName = fullQName.ToLower();
                var hash = HashHelper.GetFnv1_64Hash(fullQName);
                var hashString = hash.ToString("x");

                if (audioNameToPathMap.ContainsKey(hashString))
                {
                    ExportJsonAudioTextMap exportJsonAudioTextMap = new ExportJsonAudioTextMap();
                    exportJsonAudioTextMap.ChatContent = randomDialog.ChatContent.Count>0?randomDialog.ChatContent[0]:null;
                    exportJsonAudioTextMap.ActorName = randomDialog.AvatarName;
                    exportJsonAudioTextMap.AudioHashString = hashString;
                    
                    finalList.List.Add(exportJsonAudioTextMap);
                    bFind = true;
                    SuccessNumber2++;
                    break;
                }
            }
            if (!bFind)
            {
                FailedNumber2++;
            }
        }
        Console.WriteLine("Failed number2: {0}",FailedNumber2);
        Console.WriteLine("Success number2: {0}",SuccessNumber2);

        Int32 FailedNumber3 = 0;
        Int32 SuccessNumber3 = 0;
        foreach (var plotlineData in plotlineDataList.List)
        {
            if (string.IsNullOrEmpty(plotlineData.DialogueAudioName))
            {
                continue;
            }
            bool bFind = false;
            foreach (var auPrefix in auPrefixName)
            {
                string fullQName = auPrefix + plotlineData.DialogueAudioName+".wem";
                fullQName = fullQName.ToLower();
                var hash = HashHelper.GetFnv1_64Hash(fullQName);
                var hashString = hash.ToString("x");

                if (audioNameToPathMap.ContainsKey(hashString))
                {
                    ExportJsonAudioTextMap exportJsonAudioTextMap = new ExportJsonAudioTextMap();
                    exportJsonAudioTextMap.ChatContent = plotlineData.ChatContent.Any()?(plotlineData.ChatContent):"";
                    exportJsonAudioTextMap.ActorName = plotlineData.ActorNameID;
                    exportJsonAudioTextMap.AudioHashString = hashString;
                    
                    finalList.List.Add(exportJsonAudioTextMap);
                    bFind = true;
                    SuccessNumber3++;
                    break;
                }
            }
            if (!bFind)
            {
                FailedNumber3++;
            }
        }
        Console.WriteLine("Failed number3: {0}",FailedNumber3);
        Console.WriteLine("Success number3: {0}",SuccessNumber3);

        var str = JsonConvert.SerializeObject(finalList, Formatting.Indented);
        File.WriteAllText(Path.Combine(m_JsonResultPath, "FinallMap.json"), str);
        return true;
    }

    private string m_ResultSavePath;
    private string m_JsonResultPath;
    private string m_TextAssetPath;
    private string m_TextMapPath;
    private string m_AudioResultPath;
}

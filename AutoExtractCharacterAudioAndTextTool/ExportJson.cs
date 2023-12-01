namespace AutoExtractCharacterAudioAndTextTool;

public class CopyExtractionFile
{
    public List<string> DataFilePath;
    public List<string> AssetFilePath;
}

public class ExportJsonDialogData
{
    public string AvatarId;
    public string AudioId;
    public List<string> ChatContent;
}

public class ExportJsonDialogDataList
{
    public List<ExportJsonDialogData> List;
}

public class ExportJsonRandomDialogData
{
    public string AvatarId;
    public string AvatarName;
    public string AudioId;
    public List<string> ChatContent;
}

public class ExportJsonRandomDialogDataList
{
    public List<ExportJsonRandomDialogData> List;
}

public class ExportJsonPlotlineData
{
    public string ActorNameID;
    public string ContentID;
    public string ChatContent;    
    public string DialogID;
    public string DialogueAudioName;
}

public class ExportJsonPlotlineDataList
{
    public List<ExportJsonPlotlineData> List;
}

public class ExportJsonAudioTextMap
{
    public string ActorName;
    public string ChatContent;
    public string AudioHashString;
}

public class ExportJsonAudioTextMapList
{
    public List<ExportJsonAudioTextMap> List;
}

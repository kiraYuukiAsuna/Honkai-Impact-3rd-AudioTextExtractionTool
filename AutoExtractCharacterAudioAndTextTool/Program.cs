using System.Text;
using AutoExtractCharacterAudioAndTextTool;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

Console.WriteLine("Honkai Impact 3rd Character Audio And Text Extract Tool");

// config
string ResultSavePath = "Result";
string dataExtractPath = @"D:\AudioExtract\CharacterAudioAndText\data_extract";
string txtAssetExtractPath = @"D:\AudioExtract\CharacterAudioAndText\setting_extract\TextAsset";
string audioExtractPath = @"D:\AudioExtract\CharacterAudioAndText\Wwise-Unpacker\dest_wav";

Console.WriteLine("Extraction Start...");

ExportStep step = new ExportStep(ResultSavePath);

step.CopyTargetTextJsonFile(dataExtractPath, txtAssetExtractPath);
step.DialogDataExtract();
step.RandomDialogDataExtract();
step.PlotLineDataAkaTextMapDataExtract();
step.AudioExtract(audioExtractPath);

Console.WriteLine("Extraction End...");


/*
Console.WriteLine(HashHelper.GetHashCodeExcel("Textmap_ML29_PlotlineA01_01"));
Console.WriteLine(HashHelper.GetHashCodeExcel("Textmap_ML32_PlotlineB_410302_04"));
Console.WriteLine(HashHelper.GetHashCodeExcel("PlotlineB_312301_01_E_VO_Main_Dialog_31300_Mei"));

var result = HashHelper.GetFnv1_64Hash("Chinese_Ex\\Main\\E_VO_Main_Dialog_39378_Seele.wem".ToLower());
Console.WriteLine(result.ToString("x"));

var result2 = HashHelper.GetHashCodeExcel("E_VO_Main_Dialog_29075_Mobius");
Console.WriteLine(result2);

return;
*/
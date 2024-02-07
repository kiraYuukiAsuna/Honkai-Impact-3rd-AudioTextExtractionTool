# Honkai-Impact-3rd-AudioTextExtractionTool
A tool that help your build relationship between audio filename and character name as well as audio text. Then you can export to Json and do other things.

## How to use
Notice: this only apply to version 6.7 and before(Haven't tested after version 6.7).
- You need to get the decrypted settings and other data files in ***\AppData\LocalLow\***. For legitimate reasons, i won't provide these files. You need get it by yourself.
- Change config in program.cs

```
// config

string ResultSavePath = "Result";

string dataExtractPath = @"D:\AudioExtract\CharacterAudioAndText\data_extract";

string txtAssetExtractPath = @"D:\AudioExtract\CharacterAudioAndText\setting_extract\TextAsset";

string audioExtractPath = @"D:\AudioExtract\CharacterAudioAndText\Wwise-Unpacker\dest_wav";
```

- Run the program. Hopefully you can get the final result!

- Althought I cannot provide you with the decrypted files to extract the Audio and Text, I can give you the final result Json files. You can check 'JsonResult' folder in the root directory of this repo which hold the version 6.7 all text and audio file relations.
You may find some audio and text may not being extracted, but most of them(may be 90% ?) are correctly extracted.


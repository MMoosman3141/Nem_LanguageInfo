namespace ParseLanguageInfo {
  internal class Program {
    private static readonly string _iso639InPath = @".\RawData\iso-639-3.csv";
    private static readonly string _iso15924InPath = @".\RawData\iso-15924_Codes.csv";
    private static readonly string _unM49InPath = @".\RawData\UN_M49.csv";

    private static readonly string _iso639OutPath = @".\Output\iso-639-3.json";
    private static readonly string _iso15924OutPath = @".\Output\iso-15924.json";
    private static readonly string _unM49OutPath = @".\Output\un-m49.json";

    static void Main() {
      Iso639Reader iso639Reader = new();
      iso639Reader.ConvertCsvToJson(_iso639InPath, _iso639OutPath);

      Iso15924Reader iso15924Reader = new();
      iso15924Reader.ConvertCsvToJson(_iso15924InPath, _iso15924OutPath);

      UnM49Reader unM49Reader = new();
      unM49Reader.ConvertCsvToJson(_unM49InPath, _unM49OutPath);
    }
  }
}

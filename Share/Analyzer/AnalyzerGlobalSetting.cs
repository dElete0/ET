namespace ET.Analyzer
{
    public static class AnalyzerGlobalSetting
    {
        /// <summary>
        /// 是否开启项目的所有分析器
        /// </summary>
        public static bool EnableAnalyzer = true;
        
        //ET0020: 实体类禁止声明实体字段
        public static bool EnableAnalyzeEntityMember = false;
    }
}
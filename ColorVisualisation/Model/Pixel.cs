namespace ColorVisualisation.Model
{
    class Pixel
    {
        public int Blue { get; set; }
        public int Green { get; set; }
        public int Red { get; set; }
        public int Alpha { get; set; }

        public int IndexRow { get; set; }
        public int IndexColumn { get; set; }
        public int IndexGlobal { get; set; }

        public int RankingPoints { get; set; }
    }
}

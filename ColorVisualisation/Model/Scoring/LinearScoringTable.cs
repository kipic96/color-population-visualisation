namespace ColorVisualisation.Model.Scoring
{
    class LinearScoringTable : IScoringTable
    {
        public int GetScore(int allPosiblePlaces, int place)
        {
            return allPosiblePlaces - place;
        }
    }
}

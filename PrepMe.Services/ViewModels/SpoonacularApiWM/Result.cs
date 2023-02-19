namespace PrepMe.Services.ViewModels.SpoonacularApiWM
{
    internal class Results<T>
    {
        public List<T> results { get; set; }
        public int offset { get; set; }
        public int number { get; set; }
        public int totalResults { get; set; }
    }

}
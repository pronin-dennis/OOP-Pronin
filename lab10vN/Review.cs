namespace lab10vN
{
    internal class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public Review(int id, int productId, int rating, string comment)
        {
            Id = id;
            ProductId = productId;
            Rating = rating;
            Comment = comment;
        }
    }
}
﻿namespace BooksApp.Entities
{
    public class BookAuthor
    {
        //TODO 1: OnModelCreating fonksiyonu içerisinde bu detay varlık için  PK belişrtmeyi unutma!
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public byte Order { get; set; } //Yazarların kapakta yer alma sırası

        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }

    }
}
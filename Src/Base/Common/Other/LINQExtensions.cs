//using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Other
{
    public sealed class LINQExtensions
    {

        public T GetFirstItemFromList<T>(List<T> list, Func<T, bool> predicate) where T : class
        {
            if (predicate is null)
                return list.FirstOrDefault();
            else
                return list.FirstOrDefault(predicate);
        }

        public T GetLastItemFromList<T>(List<T> list, Func<T, bool> predicate) where T : class
        {
            if (predicate is null)
                return list.LastOrDefault();
            else
                return list.LastOrDefault(predicate);
        }

        public int GetQtdItensFromList<T>(List<T> list, Func<T, bool> predicate) where T : class
        {
            if (predicate is null)
                return list.Count();
            else
                return list.Count(predicate);
        }

        public long GetQtdItensFromBigList<T>(List<T> list, Func<T, bool> predicate) where T : class
        {
            if (predicate is null)
                return list.LongCount();
            else
                return list.LongCount(predicate);
        }

        public decimal GetTotalItensFromList<T>(List<T> list, Func<T, decimal> predicate) where T : class
        {
            return list.Sum(predicate);
        }

        public List<T> GetFirstItensFromList<T>(List<T> list, int qtyItens) where T : class
        {
            return list.Take(qtyItens) as List<T>;
        }

        public List<T> GetLastItensFromList<T>(List<T> list, int qtyItens) where T : class
        {
            return list.TakeLast(qtyItens) as List<T>;
        }

        public List<T> RemoveItemFromList<T>(List<T> list, T item) where T : class
        {
            list.Remove(item);
            return list;
        }

        public List<T> RemoveAtItemFromList<T>(List<T> list, Predicate<T> predicate) where T : class
        {
            list.RemoveAll(predicate);
            return list;
        }

        public List<T> AddItemOnFirstPlaceOfList<T>(List<T> source, T item)
        {
            var newSource = source.Prepend(item).ToList();
            return newSource;
        }

        public List<T> AddItemOnLastPlaceOfList<T>(List<T> source, T item)
        {
            var newSource = source.Append(item).ToList();
            return newSource;
        }

        public List<string> ZipList(List<int> sourceId, List<string> sourceText)
        {

            var newSource = sourceId.Zip(sourceText, (Id, Text) => Id + " - " + Text).ToList();
            return newSource;
        }


        public IEnumerable<T> ConvertArrInIEnumerable<T>(T[] array) => array.AsEnumerable();

        public IEnumerable<T> ConvertListInIEnumerable<T>(List<T> list) => list.AsEnumerable();

    }
}

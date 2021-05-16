namespace BankenKlient
{
    //Generisk lista för lagring av data
    class Lista<T>
    {
        T[] items;
        //Skapar en array
        public Lista()
        {
            items = new T[0];
        }
        //Sätter och hämtar värden för listplats index
        public T this[int index]
        {
            get
            {
                return items[index];
            }
            set 
            {
                items[index] = value;
            }
        }
        //Lägger till nytt element i listan
        public void Add(T newItem)
        {
            //Skapar en ny array med en mer plats än tidigare
            T[] newItems = new T[items.Length + 1];
            //Lägger till alla element som fanns i den gamla arrayen i den nya arrayen
            for (int index = 0; index < items.Length; index++)
            {
                newItems[index] = items[index];
            }
            //Lägger till det nya elementet på sista platsen
            newItems[newItems.Length - 1] = newItem;

            items = newItems;
        }
        //Tar bort ett element från listan
        public void Remove(int index)
        {   //Skapar en array med en mindre plats 
            T[] newItems = new T[items.Length - 1];
            //Lägger in det förra innehållet förutom den platts som ska raderas
            int j = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if (i != index)
                {
                    newItems[j] = items[i];
                    j++;
                }
            }

            items = newItems;
        }
        //Returnerar listans length
        public int Length()
        {
            return items.Length;
        }

    }
}


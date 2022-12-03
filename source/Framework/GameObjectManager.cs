using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono2.Framework
{
    static class GameObjectManager
    {
        static List<GameObject> gameObjects = new List<GameObject>();
        static List<GameObject> newGameObjects = new List<GameObject>();

        static public void clear()
        {
            gameObjects.Clear();
            newGameObjects.Clear();
        }
        private static List<GameObject> SubListOf<T>()
        {
            var returnList = new List<GameObject>();

            // object is T compares also against inheritet classes and interfaces
            IEnumerable<GameObject> query1 = gameObjects.Where(o => o is T);
            IEnumerable<GameObject> query2 = newGameObjects.Where(o => o is T);

            returnList.AddRange(query1.ToList());
            returnList.AddRange(query2.ToList());
            return returnList;
        }

        public static List<GameObject> getGameObjects<T>()
        {
            return SubListOf<T>();
        }

        public static void addGameObject(GameObject obj)
        {
            newGameObjects.Add(obj);
        }

        public static void removeGameObject(GameObject obj)
        {
            gameObjects.Remove(obj);
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            List<GameObject> survivingObjects = new List<GameObject>();
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.getAlive())
                {
                    survivingObjects.Add(gameObject);
                    gameObject.Draw(gameTime, spriteBatch);
                }
            }

            foreach (var gameObject in newGameObjects)
            {
                if (gameObject.getAlive())
                {
                    survivingObjects.Add(gameObject);
                    gameObject.Draw(gameTime, spriteBatch);
                }
            }
            newGameObjects.Clear();
            gameObjects = survivingObjects;
        }

        public static void Update(GameTime gameTime)
        {
            /*
            foreach (var gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }
            */

            Parallel.For(0, gameObjects.Count,
                  index => {
                      gameObjects[index].Update(gameTime);
                  });



        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace CharacterSelector.Scripts
{
    public class MonsterManager : SingletonBase<MonsterManager>
    {
        public MonsterInfo[] Monsters;

        public GameObject SpawnPoint;

        private int _currentIndex = 0;

        private MonsterInfo currentMonsterType = null;

        private MonsterInfo currentMonster = null;

        protected override void Init()
        {
            Persist = true;
            base.Init();
        }

        public void Start()
        {
            if (SpawnPoint != null)
            {
                SetCurrentCharacterType(_currentIndex);
            }
        }

        public void SetCurrentCharacterType(int index)
        {
            if(currentMonsterType != null)
            {
                Destroy(currentMonsterType.gameObject);
            }

            MonsterInfo character = Monsters[index];
            /*
            currentMonsterType = Instantiate<MonsterInfo>(character, 
                SpawnPoint.transform.position, Quaternion.identity);
            */
            currentMonsterType = Instantiate<MonsterInfo>(character, 
                SpawnPoint.transform.position, Quaternion.identity);

            _currentIndex = index;
        }

        public void SetCurrentCharacterType(string name)
        {
            int idx = 0;
            foreach(MonsterInfo characterInfo in Monsters)
            {
                if (characterInfo.MonsterType.Equals(name, System.StringComparison.InvariantCultureIgnoreCase))
                {
                    SetCurrentCharacterType(idx);
                    break;
                }
                idx++;
            }
        }

        public void CreateCurrentCharacter()
        {
            currentMonster = Instantiate<MonsterInfo>(currentMonsterType, SpawnPoint.transform.position, Quaternion.identity);
            
            //currentMonster = Instantiate<MonsterInfo>(currentMonsterType, new Vector3(-20,60,50), Quaternion.identity);


            currentMonster.gameObject.SetActive(false);
            //_currentCharacter.Name = name;

            DontDestroyOnLoad(currentMonster);
            DontDestroyOnLoad(SpawnPoint);

            SceneManager.LoadScene("NewMap");
        }

        public MonsterInfo GetCurrentCharacter()
        {
            return currentMonster;
        }
        
    }
}

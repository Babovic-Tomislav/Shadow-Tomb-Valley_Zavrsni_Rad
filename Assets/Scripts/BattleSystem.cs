using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, WAIT, RUN}

public class BattleSystem : MonoBehaviour
{
    public Button skillButtonCopy;
    public Image background;
    public BattleState state;
    Navigation navigation;
    public GameObject endBattlePanel;
    public List<TMP_Text> battleRewards;
    public GameObject playerBattleStation;
    public List<GameObject> enemyBattleStation;
    public EnemyUnit[] enemyPrefLabs;
    public int numberOfEnemies;
    public List<EnemyUnit> enemies;

    PlayerOptionsControl playerOptionsControl;
    CustomScroll customScroll;

    public TMP_Text turn;

    public Button itemButton;
    public Button skillButton;
    public List<Button> items;
    public List<Button> skills;
    public List<Button> skillsCopy;

    public Button back;
    public Button deactivateButtons;

    public Button lastSelectedEnemy;
    public Button playerAttack;

    public List<Button> classes;
    public TMP_Text classDescription;
    public GameObject classPanel;

    public float clipTime;

    int enemyToAttack = 0;
    bool isDead;
    bool manaChanged = true;

    int gold;
    int exp;

    private void Awake()
    {
        skills  = new List<Button>(); 
        items  = new List<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {


        PauseGame.canPause = false;
        
        state = BattleState.START;
        
        if (PlayerUnit.bossFight)
            StartCoroutine(SetupBossBattle());
        else
        StartCoroutine(SetupBattle());

    }

    IEnumerator SetupBossBattle()
    {
        clipTime = Audio.audioPlayer.audioSource.time;
        Audio.audioPlayer.audioSource.clip = Resources.Load<AudioClip>("Audio/boss");
        Audio.audioPlayer.audioSource.time = 0;
        Audio.audioPlayer.audioSource.Play();
        SetUpSkills();
        SetUpItems();
        var boss = Resources.Load<BossUnit>("Battle/Boss/ShadowKing");

        enemyBattleStation[0].SetActive(true);
        enemies.Add(Instantiate(boss, enemyBattleStation[0].transform));
        

        enemyBattleStation[0].GetComponent<BattleHUD>().SetEnemyHud(enemies[0]);
        enemyBattleStation[0].GetComponent<Image>().sprite = enemies[0].GetComponent<Image>().sprite;
        enemyBattleStation[0].GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);

        
        playerBattleStation.GetComponent<BattleHUD>().SetHUD();

        
        enemyBattleStation[0].SetActive(true);

        yield return new WaitForSeconds(1f);
        state = BattleState.PLAYERTURN;
        
        PlayerTurn();
    }

    IEnumerator SetupBattle()
    {
        clipTime = Audio.audioPlayer.audioSource.time;
        Audio.audioPlayer.audioSource.clip = Resources.Load<AudioClip>("Audio/battle");
        Audio.audioPlayer.audioSource.time = 0;
        Audio.audioPlayer.audioSource.Play();
        SetUpSkills();
        SetUpItems();
        if (PlayerPrefs.GetString("_last_scene_") == "World")
            background.sprite = Resources.Load<Sprite>("Battle/BackgroundImages/worldbkgr");
        else
            background.sprite = Resources.Load<Sprite>("Battle/BackgroundImages/forestbkgr");

        enemyPrefLabs = Resources.LoadAll<EnemyUnit>("Battle/Enemies");
        if (PlayerPrefs.GetString("_last_scene_") == "World")
        {
            numberOfEnemies = UnityEngine.Random.Range(0, 1000);
            if (numberOfEnemies < 700)
                numberOfEnemies = 1;
            else if (numberOfEnemies < 900)
                numberOfEnemies = 2;
            else
                numberOfEnemies = 3;
        }
        else
        {
            numberOfEnemies = UnityEngine.Random.Range(0, 1000);
            if (numberOfEnemies < 500)
                numberOfEnemies = 1;
            else if (numberOfEnemies < 800)
                numberOfEnemies = 2;
            else
                numberOfEnemies = 3;
        }
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemyBattleStation[i].SetActive(true);
            enemies.Add(Instantiate(enemyPrefLabs[UnityEngine.Random.Range(0, 3)], enemyBattleStation[i].transform));
            if (PlayerPrefs.GetString("_last_scene_") == "World")
                enemies[i].SetStats(UnityEngine.Random.Range(1, 3));
            else
                enemies[i].SetStats(UnityEngine.Random.Range(2, 5));
            
            enemyBattleStation[i].GetComponent<BattleHUD>().SetEnemyHud(enemies[i]);
            enemyBattleStation[i].GetComponent<Image>().sprite = enemies[i].GetComponent<Image>().sprite;
        }
        playerBattleStation.GetComponent<BattleHUD>().SetHUD();




        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        state = BattleState.WAIT;
        if (playerAttack.GetComponent<SkillButton>())
        {
            manaChanged = true;
            PlayerUnit.SetMana(-playerAttack.GetComponent<SkillButton>().skill.manaCost);
            playerBattleStation.GetComponent<BattleHUD>().SetMana(PlayerUnit.manaAmount);
            isDead = lastSelectedEnemy.GetComponentInChildren<EnemyUnit>().TakeDmg
                (Mathf.RoundToInt(playerAttack.GetComponent<SkillButton>().skill.skillDmg * PlayerUnit.damage));
            lastSelectedEnemy.GetComponentInChildren<Animator>().SetTrigger(playerAttack.GetComponent<SkillButton>().skill.skillName);

        }
        else
        {
            manaChanged = false;
            isDead = lastSelectedEnemy.GetComponentInChildren<EnemyUnit>().TakeDmg
                (PlayerUnit.damage);


            lastSelectedEnemy.GetComponentInChildren<Animator>().SetTrigger("Attack");

        }
        
        
        lastSelectedEnemy.GetComponent<BattleHUD>().SetHP(lastSelectedEnemy.GetComponentInChildren<EnemyUnit>().currentHP);
        yield return new WaitForSeconds(1);
       
        if (isDead)
        {
            gold += lastSelectedEnemy.GetComponentInChildren<EnemyUnit>().gold;
            exp += lastSelectedEnemy.GetComponentInChildren<EnemyUnit>().exp;
            foreach (var enemy in enemyBattleStation)
            {
                if (enemy.GetComponent<Button>() == lastSelectedEnemy)
                {
                    enemies.RemoveAt(enemyBattleStation.IndexOf(enemy));
                    enemyBattleStation[enemyBattleStation.IndexOf(enemy)].GetComponent<BattleHUD>().DestroyHud();
                    Destroy(enemyBattleStation[enemyBattleStation.IndexOf(enemy)].gameObject);
                    enemyBattleStation.Remove(enemy);

                    break;
                }
            }
            if (enemies.Count == 0)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
        }
        if(enemies.Count !=0)
        {

            enemyToAttack = 0;
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        turn.text = "Enemy turn";
        yield return new WaitForSeconds(1f);
        
        isDead = PlayerUnit.TakeDmg(enemies[enemyToAttack].damage);
        playerBattleStation.GetComponentInChildren<Animator>().SetTrigger("Attack");
        playerBattleStation.GetComponent<BattleHUD>().SetHP(PlayerUnit.currentHP);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else if (enemyToAttack == enemies.Count-1)
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        else
        {
            enemyToAttack++;
            StartCoroutine(EnemyTurn());
            
        }

    }

    IEnumerator EndBattle()
    {
        
        PauseGame.canPause = true;
        if (state == BattleState.RUN)
        {
            Audio.audioPlayer.audioSource.clip = Resources.Load<AudioClip>("Audio/main");
            Audio.audioPlayer.audioSource.time = clipTime;
            Audio.audioPlayer.audioSource.Play();
            SceneManager.LoadScene(PlayerPrefs.GetString("_last_scene_"), LoadSceneMode.Single);
            
            
        }
        else if (state==BattleState.WON)
        {

            if (PlayerUnit.bossFight)
            {
                DestroyOnLoad.boss = true;
                PlayerUnit.bossFight = false;
            }
            endBattlePanel.SetActive(true);
            battleRewards[0].text = gold.ToString();
            battleRewards[1].text = exp.ToString();
            battleRewards[2].text = "";
            for (int j = 0; j < numberOfEnemies; j++)
            {
                int i = UnityEngine.Random.Range(0, 1000);
                if (i < 850)
                    battleRewards[2].gameObject.SetActive(false);
                else if (i < 950)
                {
                    battleRewards[2].gameObject.SetActive(true);
                    var item = Resources.LoadAll<DefaultItem>("Items");
                    var itemToPickUp = item[Random.Range(1, item.Length)];
                    Inventory.AddItem(itemToPickUp);
                    battleRewards[2].text += "An enemy droped  " + itemToPickUp.itemName+"\n";
                }
                else
                {
                    battleRewards[2].gameObject.SetActive(true);
                    var item = Resources.LoadAll<EquipmentItem>("Items");
                    var itemToPickUp = item[Random.Range(1, item.Length)];
                    Inventory.AddItem(itemToPickUp);
                    battleRewards[2].text += "An enemy droped  " + itemToPickUp.itemName+"\n";
                }
            }
            Inventory.goldAmount += gold;
            PlayerUnit.SetExp(exp);

            if (PlayerUnit.pickUpClass)
            {
                
                classPanel.SetActive(true);
                EventSystem.current.SetSelectedGameObject(classes[0].gameObject);
                EventSystem.current.currentSelectedGameObject.GetComponent<Button>().OnSelect(null);
                while (PlayerUnit.pickUpClass)
                {
                    
                    if (EventSystem.current.currentSelectedGameObject == classes[0].gameObject)
                    {
                        
                        classDescription.text = classes[0].GetComponent<ClassButton>().playerClass.description;
                    }
                    else
                    {
                        classDescription.text = classes[1].GetComponent<ClassButton>().playerClass.description;
                    }
                    yield return null;
                }
                classPanel.SetActive(false);
            }
            yield return new WaitForSeconds(2f);
            PlayerUnit.enemiesKilled += numberOfEnemies;
            Audio.audioPlayer.audioSource.clip = Resources.Load<AudioClip>("Audio/main");
            Audio.audioPlayer.audioSource.time = clipTime;
            Audio.audioPlayer.audioSource.Play();
            SceneManager.LoadScene(PlayerPrefs.GetString("_last_scene_"), LoadSceneMode.Single);
        }
        else
        {
            Audio.audioPlayer.audioSource.clip = Resources.Load<AudioClip>("Audio/main");
            Audio.audioPlayer.audioSource.time = clipTime;
            Audio.audioPlayer.audioSource.Play();
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }

 

    public void OnAttackButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (state != BattleState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerAttack());
    }

    void PlayerTurn()
    {
       
        turn.text = "Player turn";
        if(manaChanged)
        SetSkillsInteractable();
        BackButton();

    }

    public void OnRunButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (state != BattleState.PLAYERTURN)
            return;

        int rint = UnityEngine.Random.Range(0, 3);
        if (rint == 2)
        {
            state = BattleState.RUN;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

        
    }

    public void SelectEnemy()
    {
        enemyBattleStation[0].GetComponent<Button>().Select();
    }

    public void SetUpSkills()
    {
        foreach(var skill in skills)
        {
            Destroy(skill.gameObject);
        }
        foreach (var skill in Inventory.playerSkills)
        {
            Button button = Instantiate(skillButton) as Button;
            button.gameObject.SetActive(true);

            button.GetComponent<SkillButton>().SetName(skill.skillName);

            button.GetComponent<SkillButton>().SetSprite(skill.img);
            button.GetComponent<SkillButton>().SetDescription(skill.description);
            button.GetComponent<SkillButton>().SetSkill(skill);

            button.transform.SetParent(skillButton.transform.parent, false);
            skills.Add(button);

            Button button2 = Instantiate(skillButtonCopy)as Button;
            button2.gameObject.SetActive(true);

            
            var col = button2.colors;
            col.pressedColor = Color.gray;
            button2.colors = col;
            

            button2.GetComponent<SkillButton>().SetName(skill.skillName);

            button2.GetComponent<SkillButton>().SetSprite(skill.img);
            button2.GetComponent<SkillButton>().SetDescription(skill.description);
            button2.GetComponent<SkillButton>().SetSkill(skill);

            button2.transform.SetParent(button.transform, false);

        }
    }

    public void SetUpItems()
    {
        
        foreach(var item in items)
        {
            Destroy(item.gameObject);
        }
        items = new List<Button>();
        int i = items.Count;
        foreach (var item in Inventory.playerInventory)
        {

            if (item.type == ItemObject.ItemType.Equipment) continue;
            Button button = Instantiate(itemButton) as Button;
            button.gameObject.SetActive(true);

            button.name = item.itemName;
            button.GetComponent<ItemButton>().SetName(item.itemName);
            button.GetComponent<ItemButton>().SetQuantity(Inventory.itemQuantity[i]);
            button.GetComponent<ItemButton>().SetSprite(item.sprit);
            button.GetComponent<ItemButton>().SetDescription(item.description);
            button.GetComponent<ItemButton>().SetItem(item);
            button.transform.SetParent(itemButton.transform.parent, false);
            items.Add(button);

            

            if (i > 0)
            {

                navigation = button.navigation;
                navigation.mode = Navigation.Mode.Explicit;
                navigation.selectOnLeft = items[i - 1];
                items[i ].navigation = navigation;
                navigation = items[i - 1].navigation;
                navigation.mode = Navigation.Mode.Explicit;
                navigation.selectOnRight = items[i ];
                items[i - 1].navigation = navigation;
                if (i > 1)
                {
                    navigation = button.navigation;
                    navigation.mode = Navigation.Mode.Explicit;
                    navigation.selectOnUp = items[i - 2];
                    items[i ].navigation = navigation;
                    navigation = items[i - 2].navigation;
                    navigation.mode = Navigation.Mode.Explicit;
                    navigation.selectOnDown = items[i ];
                    items[i - 2].navigation = navigation;
                }
            }
            if (i == items.Count-1)
            {
                navigation = items[i ].GetComponent<Button>().navigation;
                navigation.mode = Navigation.Mode.Explicit;
                navigation.selectOnRight = back;
                items[i ].GetComponent<Button>().navigation = navigation;
            }
            i++;
        }
    }

    public void SetLastSelectedEnemy(Button enemy)
    {
        lastSelectedEnemy = enemy;
    }

    public void SetPlayerAttack(Button attack)
    {
        playerAttack = attack;
    }

    public void SetSkillsInteractable()
    {
        bool resetNavigation = false;
        for(int i =0;i<skills.Count;i++)
        {
            skills[i].GetComponent<Button>().interactable = true;


            if (skills[i].GetComponent<SkillButton>().skill.manaCost > PlayerUnit.manaAmount)
            {
                skills[i].GetComponent<Button>().interactable = false;
                skills[i].GetComponentsInChildren<Button>()[1].interactable=true;
                resetNavigation = true;
            }else
                skills[i].GetComponentsInChildren<Button>()[1].interactable=false;

        }
        if(resetNavigation )
        {
            for (int i =0;i<skills.Count;i++)
            {
                if (i > 1)
                {
                    if (skills[i].GetComponent<Button>().interactable)
                        navigation = skills[i].navigation;
                    else
                        navigation = skills[i].GetComponentsInChildren<Button>()[1].navigation;
                    navigation.mode = Navigation.Mode.Explicit;
                    if(skills[i-2].GetComponent<Button>().interactable)
                    navigation.selectOnLeft = skills[i - 1];
                    else
                        navigation.selectOnLeft = skills[i - 1].GetComponentsInChildren<Button>()[1];
                    if (skills[i].GetComponent<Button>().interactable)
                        skills[i].navigation = navigation;
                    else
                        skills[i].GetComponentsInChildren<Button>()[1].navigation = navigation;
                    if (skills[i - 1].GetComponent<Button>().interactable)
                        navigation = skills[i - 1].navigation;
                    else
                        navigation = skills[i - 1].GetComponentsInChildren<Button>()[1].navigation;
                    navigation.mode = Navigation.Mode.Explicit;
                    if (skills[i].GetComponent<Button>().interactable)
                        navigation.selectOnRight = skills[i];
                    else
                        navigation.selectOnRight = skills[i].GetComponentsInChildren<Button>()[1];
                    if(skills[i-2].GetComponent<Button>().interactable)
                    skills[i - 1].navigation = navigation;
                    else
                        skills[i - 1].GetComponentsInChildren<Button>()[1].navigation = navigation;
                    if (i > 2)
                    {
                        if (skills[i].GetComponent<Button>().interactable)
                            navigation = skills[i].navigation;
                        else
                            navigation = skills[i].GetComponentsInChildren<Button>()[1].navigation;
                        navigation.mode = Navigation.Mode.Explicit;
                        if (skills[i - 2].GetComponent<Button>().interactable)
                            navigation.selectOnUp = skills[i - 2];
                        else
                            navigation.selectOnUp = skills[i - 2].GetComponentsInChildren<Button>()[1];
                        if (skills[i].GetComponent<Button>().interactable)
                            skills[i].navigation = navigation;
                        else
                            skills[i].GetComponentsInChildren<Button>()[1].navigation = navigation;
                        if (skills[i - 2].GetComponent<Button>().interactable)
                            navigation = skills[i - 2].navigation;
                        else
                            navigation = skills[i - 2].GetComponentsInChildren<Button>()[1].navigation;
                        navigation.mode = Navigation.Mode.Explicit;
                        if (skills[i].GetComponent<Button>().interactable)
                            navigation.selectOnDown = skills[i];
                        else
                            navigation.selectOnDown = skills[i].GetComponentsInChildren<Button>()[1];
                        if (skills[i - 2].GetComponent<Button>().interactable)
                            skills[i - 2].navigation = navigation;
                        else
                            skills[i - 3].GetComponentsInChildren<Button>()[1].navigation = navigation;
                    }
                }
                if(i==skills.Count-1)
                {
                    if(skills[i].GetComponent<Button>().interactable)
                    {
                        navigation = skills[i ].navigation;
                        navigation.mode = Navigation.Mode.Explicit;
                        navigation.selectOnRight = back;
                    }
                    else
                    {
                        navigation = skills[i].GetComponentsInChildren<Button>()[1].navigation;
                        navigation.mode = Navigation.Mode.Explicit;
                        navigation.selectOnRight = back;
                    }
                }
            }
        }


    }
    public void DeactivateButtons()
    {
        deactivateButtons.onClick.Invoke();
    }

    public void BackButton()
    {
        back.onClick.Invoke();
        
    }

    public void ActivateSkills(bool activeDeactive)
    {
        if (activeDeactive)
            foreach (var skill in skills)
                skill.interactable = true;
        else
            foreach (var skill in skills)
                skill.interactable = false;
    }

    public void ActivateItems(bool activeDeactive)
    {
        if(activeDeactive)
        foreach(var item in items)
            item.interactable = true;
        else
            foreach (var item in items)
                item.interactable = false;
    }

    public void OnItemUse()
    {
        SetUpItems();
        playerBattleStation.GetComponent<BattleHUD>().SetHP(PlayerUnit.currentHP);
        playerBattleStation.GetComponent<BattleHUD>().SetMana(PlayerUnit.manaAmount);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        
    }

    public void SelectFirstItem()
    {
        if (items.Count!=0)
            items[0].Select();
        else
        {
            back.gameObject.SetActive(true);
            back.Select();
        }
    }

    public void SelectFirstSkill()
    {
        if (skills.Count!=0)
            if (skills[0].GetComponent<Button>().interactable)
                skills[0].Select();
            else
                skills[0].GetComponentsInChildren<Button>()[1].Select();
        else
        {
            back.gameObject.SetActive(true);
            back.Select();
        }
    }

    public void ChooseClass(ClassObject classObject)
    {
        Inventory.AddClass(classObject);
    }

}

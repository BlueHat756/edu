using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static CSVDataReader;

public enum SceneName
{
    SMAPLE_SCENE,
    MAIN_SCENE,
    BATTLE_SCENE,
}

    
public class CSVDataReader : Singleton<CSVDataReader>//�̱����� ��� �޴´�. �⺻�����δ� MonoBehaviour(�������̺��)�� �޷�����
{
    

    #region Data Tables
    [Header("���� ���̺� �ִ� ��")]
    public TextAsset StUPtbl;
    [Header("���� ���̺� �ִ� ��")]
    public TextAsset LvStUptbl;
    [Header("Ŭ���� ���̺� �ִ� ��")]
    public TextAsset Classtbl;
    public TextAsset Exptbl;
    public TextAsset Stagetbl;
    public TextAsset Wavetbl;
    public TextAsset Spawntbl;
    public TextAsset Risktbl;
    public TextAsset Monstertbl;
    public TextAsset Projectiletbl;
    public TextAsset Skilltbl;
    public TextAsset Effecttbl;

    #endregion

    #region Data Dicrionary
    public Dictionary<int, StUPData> stUPData = new Dictionary<int, StUPData>();//��ǻ� �ʿ����
    public Dictionary<float, LvStUpData> lvStUpData = new Dictionary<float, LvStUpData>();
    public Dictionary<int, ClassData> classData = new Dictionary<int, ClassData>();
    public Dictionary<int, ExpData> expData = new Dictionary<int, ExpData>();
    public Dictionary<int, StageData> stageData = new Dictionary<int, StageData>();
    public Dictionary<int, WaveData> waveData = new Dictionary<int, WaveData>();
    public Dictionary<int, SpawnData> spawnData = new Dictionary<int, SpawnData>();
    public Dictionary<int, MonsterData> monsterData = new Dictionary<int, MonsterData>();
    public Dictionary<int, ProjectileData> projectileData = new Dictionary<int, ProjectileData>();
    public Dictionary<int, RiskData> riskData = new Dictionary<int, RiskData>();
    public Dictionary<int, SkillData> skillData = new Dictionary<int, SkillData>();
    public Dictionary<int, EffectData> effectData = new Dictionary<int, EffectData>();
    public Dictionary<string, GameObject> prefabData = new Dictionary<string, GameObject>();
    public Dictionary<string, Sprite> spriteData = new Dictionary<string, Sprite>();

    #endregion

    #region User Data
    public int curCharId;

    #endregion

    #region Game Data

    #endregion


    void Start()
    {
        curCharId = PlayerPrefs.GetInt("charId", 1);
        StUPDataLoad();
        LvStUpDataLoad();
        ClassDataLoad();
        ExpDataLoad();
        StageDataLoad();
        WaveDataLoad();
        SpawnDataLoad();
        RiskDataLoad();
        MonsterDataLoad();
        ProjectileDataLoad();
        SkillDataLoad();
        EffectDataLoad();

        MoveScene(SceneName.MAIN_SCENE);
      
    }

    public int ReturnId ()
    {
        return curCharId;
    }

    public int ReturnId(bool isLeft)
    {
        int _id =curCharId;

        int dataCnt = classData.Count;

        if(isLeft)
        {
            //���� (������ -1)
            _id--;//1 > ó���� ����
        }
        else
        {
            //���� (������ +1)
            _id++;// 4 < ó���� ����
        }

        if (_id == dataCnt + 1)//�߰����� if������ ���� �ȵǾ��ִ� ��Ҹ� ó��
            _id = 1;
        else if (_id == 0)
            _id = dataCnt;
 
        PlayerPrefs.SetInt("charId", _id);

        curCharId = _id;

        return curCharId;
    }

    IEnumerator ChangeScene(string _scene)
    {
        while (true) //�� ���� ���� �ݺ���, (��:��� �ݺ�, ����:Ż��)
        { 
            yield return new WaitForSeconds(3);//���ҽ� �ε尡 �� �Ǿ����� Ȯ�� �� ���� �� �� ������ ��ɵ��� ������
            Debug.LogWarning("dd");
            break;
        }
      
        SceneManager.LoadScene( _scene );
    }

    
    public void MoveScene(SceneName sceneName)
    {
        string scene = "";
        // ���⿡ �������� �̵��ض� �ϴºκ�
        switch (sceneName)
        {
            
            case SceneName.SMAPLE_SCENE:
                scene = "00_SampleScene";
                break;

            case SceneName.BATTLE_SCENE:
                scene = "02_Battle";
                break;

            case SceneName.MAIN_SCENE:
                scene = "01_Main";
                break;

        }
        IEnumerator _changeScene = ChangeScene(scene);
        StartCoroutine (ChangeScene(scene));

        StopCoroutine(ChangeScene(scene));
    }

    void StUPDataLoad()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(StUPtbl); for (int i = 0; i < data.Count; i++)
        {
            StUPData stData = new StUPData();
            stData.startLv = int.Parse(data[i]["startLv"].ToString());
            stData.upPrb = float.Parse(data[i]["upPrb"].ToString());
            stData.useWon = int.Parse(data[i]["useWon"].ToString());
            stData.upMaxHp = float.Parse(data[i]["upMaxHp"].ToString());
            stData.upAtkPwr = float.Parse(data[i]["upAtkPwr"].ToString());
            stData.upCriPrb = float.Parse(data[i]["upCriPrb"].ToString());
            stData.upCriDmg = float.Parse(data[i]["upCriDmg"].ToString());
            stData.upHpRcv = float.Parse(data[i]["upHpRcv"].ToString());
            stData.upCdnPct = float.Parse(data[i]["upCdnPct"].ToString());
            stUPData.Add(stData.startLv, stData);
        }
    }
    void LvStUpDataLoad()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(LvStUptbl); for (int i = 0; i < data.Count; i++)
        {
            LvStUpData stData = new LvStUpData();
            stData.maxHp = float.Parse(data[i]["maxHp"].ToString());
            stData.atkPwr = float.Parse(data[i]["atkPwr"].ToString());
            stData.criPrb = float.Parse(data[i]["criPrb"].ToString());
            stData.criDmg = float.Parse(data[i]["criDmg"].ToString());
            stData.hpRcv = float.Parse(data[i]["hpRcv"].ToString());
            stData.cdnPct = float.Parse(data[i]["cdnPct"].ToString());
            lvStUpData.Add(stData.maxHp, stData);
        }
    }
    void ClassDataLoad()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(Classtbl); for (int i = 0; i < data.Count; i++)
        {
            ClassData stData = new ClassData();
            stData.id = int.Parse(data[i]["id"].ToString());
            stData.className = data[i]["className"].ToString();
            stData.maxHp = float.Parse(data[i]["maxHp"].ToString());
            stData.atkPwr = float.Parse(data[i]["atkPwr"].ToString());
            stData.criPrb = float.Parse(data[i]["criPrb"].ToString());
            stData.criDmg = float.Parse(data[i]["criDmg"].ToString());
            stData.hpRcv = float.Parse(data[i]["hpRcv"].ToString());
            stData.cdnPct = float.Parse(data[i]["cdnPct"].ToString());
            stData.gotDmg = float.Parse(data[i]["gotDmg"].ToString());
            stData.moveSpeed = float.Parse(data[i]["moveSpeed"].ToString());
            stData.skill_01 = int.Parse(data[i]["skill_01"].ToString());
            stData.skill_02 = int.Parse(data[i]["skill_02"].ToString());
            stData.skill_03 = int.Parse(data[i]["skill_03"].ToString());
            stData.skill_04 = int.Parse(data[i]["skill_04"].ToString());
            stData.skill_05 = int.Parse(data[i]["skill_05"].ToString());
            stData.skill_06 = int.Parse(data[i]["skill_06"].ToString());
            stData.skill_07 = int.Parse(data[i]["skill_07"].ToString());
            stData.skill_08 = int.Parse(data[i]["skill_08"].ToString());
            classData.Add(stData.id, stData);
        }
    }
    void ExpDataLoad()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(Exptbl); for (int i = 0; i < data.Count; i++)
        {
            ExpData stData = new ExpData();
            stData.lv = int.Parse(data[i]["lv"].ToString());
            stData.curExp = int.Parse(data[i]["curExp"].ToString());
            expData.Add(stData.lv, stData);
        }
    }
    void StageDataLoad()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(Stagetbl); for (int i = 0; i < data.Count; i++)
        {
            StageData stData = new StageData();
            stData.id = int.Parse(data[i]["id"].ToString());
            stData.waveId_01 = int.Parse(data[i]["waveId_01"].ToString());
            stData.waveId_02 = int.Parse(data[i]["waveId_02"].ToString());
            stData.waveId_03 = int.Parse(data[i]["waveId_03"].ToString());
            stData.waveId_04 = int.Parse(data[i]["waveId_04"].ToString());
            stData.waveId_05 = int.Parse(data[i]["waveId_05"].ToString());
            stData.waveId_06 = int.Parse(data[i]["waveId_06"].ToString());
            stageData.Add(stData.id, stData);
        }
    }
    void WaveDataLoad()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(Wavetbl); for (int i = 0; i < data.Count; i++)
        {
            WaveData stData = new WaveData();
            stData.id = int.Parse(data[i]["id"].ToString());
            stData.waveType = int.Parse(data[i]["waveType"].ToString());
            stData.monId_01 = int.Parse(data[i]["monId_01"].ToString());
            stData.monId_02 = int.Parse(data[i]["monId_02"].ToString());
            stData.monId_03 = int.Parse(data[i]["monId_03"].ToString());
            stData.monId_04 = int.Parse(data[i]["monId_04"].ToString());
            stData.monId_05 = int.Parse(data[i]["monId_05"].ToString());
            stData.monId_06 = int.Parse(data[i]["monId_06"].ToString());
            stData.specialMonId_01 = int.Parse(data[i]["specialMonId_01"].ToString());
            stData.specialMonId_02 = int.Parse(data[i]["specialMonId_02"].ToString());
            stData.spawn_01 = int.Parse(data[i]["spawn_01"].ToString());
            stData.spawn_02 = int.Parse(data[i]["spawn_02"].ToString());
            stData.spawn_03 = int.Parse(data[i]["spawn_03"].ToString());
            stData.spawn_04 = int.Parse(data[i]["spawn_04"].ToString());
            waveData.Add(stData.id, stData);
        }
    }
    void SpawnDataLoad()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(Spawntbl); for (int i = 0; i < data.Count; i++)
        {
            SpawnData stData = new SpawnData();
            stData.id = int.Parse(data[i]["id"].ToString());
            stData.monNum_01 = int.Parse(data[i]["monNum_01"].ToString());
            stData.monNum_02 = int.Parse(data[i]["monNum_02"].ToString());
            stData.monNum_03 = int.Parse(data[i]["monNum_03"].ToString());
            stData.monNum_04 = int.Parse(data[i]["monNum_04"].ToString());
            stData.monNum_05 = int.Parse(data[i]["monNum_05"].ToString());
            spawnData.Add(stData.id, stData);
        }
    }
    void RiskDataLoad()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(Risktbl); for (int i = 0; i < data.Count; i++)
        {
            RiskData stData = new RiskData();
            stData.id = int.Parse(data[i]["id"].ToString());
            stData.name = data[i]["name"].ToString();
            stData.curHp = float.Parse(data[i]["curHp"].ToString());
            stData.maxHp = float.Parse(data[i]["maxHp"].ToString());
            stData.atkPwr = float.Parse(data[i]["atkPwr"].ToString());
            stData.moveSpeed = float.Parse(data[i]["moveSpeed"].ToString());
            stData.criPrb = float.Parse(data[i]["criPrb"].ToString());
            stData.criDmg = float.Parse(data[i]["criDmg"].ToString());
            stData.gotDmg = float.Parse(data[i]["gotDmg"].ToString());
            stData.hpRcv = float.Parse(data[i]["hpRcv"].ToString());
            stData.cdnPct = float.Parse(data[i]["cdnPct"].ToString());
            stData.rmnRnd = int.Parse(data[i]["rmnRnd"].ToString());
            stData.wonScale = float.Parse(data[i]["wonScale"].ToString());
            riskData.Add(stData.id, stData);
        }
    }
    void MonsterDataLoad()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(Monstertbl); for (int i = 0; i < data.Count; i++)
        {
            MonsterData stData = new MonsterData();
            stData.id = int.Parse(data[i]["id"].ToString());
            stData.name = data[i]["name"].ToString();
            stData.monType = int.Parse(data[i]["monType"].ToString());
            stData.projectile_01 = int.Parse(data[i]["projectile_01"].ToString());
            stData.projectile_02 = int.Parse(data[i]["projectile_02"].ToString());
            stData.giveWon = float.Parse(data[i]["giveWon"].ToString());
            stData.giveExp = float.Parse(data[i]["giveExp"].ToString());
            stData.MaxHp = float.Parse(data[i]["MaxHp"].ToString());
            stData.atkPower = float.Parse(data[i]["atkPower"].ToString());
            stData.moveSpeed = float.Parse(data[i]["moveSpeed"].ToString());
            stData.useSkill_01 = int.Parse(data[i]["useSkill_01"].ToString());
            stData.useSkill_02 = int.Parse(data[i]["useSkill_02"].ToString());
            stData.useSkill_03 = int.Parse(data[i]["useSkill_03"].ToString());
            stData.useSkill_04 = int.Parse(data[i]["useSkill_04"].ToString());
            stData.useSkill_05 = int.Parse(data[i]["useSkill_05"].ToString());
            stData.useSkill_06 = int.Parse(data[i]["useSkill_06"].ToString());
            stData.useSkill_07 = int.Parse(data[i]["useSkill_07"].ToString());
            stData.useSkill_08 = int.Parse(data[i]["useSkill_08"].ToString());
            monsterData.Add(stData.id, stData);
        }
    }
    void ProjectileDataLoad()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(Projectiletbl); for (int i = 0; i < data.Count; i++)
        {
            ProjectileData stData = new ProjectileData();
            stData.id = int.Parse(data[i]["id"].ToString());
            stData.name = data[i]["name"].ToString();
            projectileData.Add(stData.id, stData);
        }
    }
    void SkillDataLoad()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(Skilltbl); for (int i = 0; i < data.Count; i++)
        {
            SkillData stData = new SkillData();
            stData.id = int.Parse(data[i]["id"].ToString());
            stData.skillName = data[i]["skillName"].ToString();
            stData.skillEffect_01 = int.Parse(data[i]["skillEffect_01"].ToString());
            stData.skillEffect_02 = int.Parse(data[i]["skillEffect_02"].ToString());
            stData.stiffTime = float.Parse(data[i]["stiffTime"].ToString());
            stData.stunTime = float.Parse(data[i]["stunTime"].ToString());
            stData.dotTime = float.Parse(data[i]["dotTime"].ToString());
            stData.dotPeriod = float.Parse(data[i]["dotPeriod"].ToString());
            stData.dotDmgPct = float.Parse(data[i]["dotDmgPct"].ToString());
            stData.slowTime = float.Parse(data[i]["slowTime"].ToString());
            stData.slowPct = float.Parse(data[i]["slowPct"].ToString());
            stData.skillCool = float.Parse(data[i]["skillCool"].ToString());
            stData.skillTime = float.Parse(data[i]["skillTime"].ToString());
            stData.dmgPct_01 = float.Parse(data[i]["dmgPct_01"].ToString());
            stData.dmgPct_02 = float.Parse(data[i]["dmgPct_02"].ToString());
            stData.dmgPct_03 = float.Parse(data[i]["dmgPct_03"].ToString());
            stData.dmgPct_04 = float.Parse(data[i]["dmgPct_04"].ToString());
            stData.dmgPct_05 = float.Parse(data[i]["dmgPct_05"].ToString());
            skillData.Add(stData.id, stData);
        }
    }
    void EffectDataLoad()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(Effecttbl); for (int i = 0; i < data.Count; i++)
        {
            EffectData stData = new EffectData();
            stData.id = int.Parse(data[i]["id"].ToString());
            stData.effectName = data[i]["effectName"].ToString();
            effectData.Add(stData.id, stData);
        }
    }



    public class StUPData
    {
        public int startLv;  //��ȭ ���� ����
        public float upPrb;  //���� Ȯ��
        public int useWon;  //��ȭ �õ� ���
        public float upMaxHp;  //�ִ� ü�� ��·�
        public float upAtkPwr;  //���ݷ� ��·�
        public float upCriPrb;  //ġ��Ÿ Ȯ�� ��·�
        public float upCriDmg;  //ġ��Ÿ ������ ��·�
        public float upHpRcv;  //�ʴ� ü�� ȸ���� ��·�
        public float upCdnPct;  //��ų ��Ÿ�� ������ ��·�

    }

    public class LvStUpData
    {
        public float maxHp;  //������ �� �����ϴ� �ִ� ü�� ��
        public float atkPwr;  //������ �� �����ϴ� ���ݷ� ��
        public float criPrb;  //5�������� ����, 10���� ������ �� �����ϴ� ġ��Ÿ Ȯ�� ��
        public float criDmg;  //5�������� ����, 10���� ������ �� �����ϴ� ġ��Ÿ ������ ��
        public float hpRcv;  //������ �� �����ϴ� �ʴ� ü�� ȸ���� ��
        public float cdnPct;  //5�������� ����, 10���� ������ �� �����ϴ� ��ų ��Ÿ�� ������ ��
    }

    public class ClassData
    {
        public int id;  //id
        public string className;  //Ŭ���� �̸�
        public float maxHp;  //���� �� �ִ� ü��
        public float atkPwr;  //���� �� ���ݷ�
        public float criPrb;  //���� �� ġ��Ÿ Ȯ��
        public float criDmg;  //���� �� ġ��Ÿ ������
        public float hpRcv;  //���� �� �ʴ� ü�� ȸ����
        public float cdnPct;  //���� �� ��ų ��Ÿ�� ������
        public float gotDmg;  //���� �� �޴� ���ط�
        public float moveSpeed;  //���� �� �̵� �ӵ�
        public int skill_01;  //Skill.tbl�� id ����
        public int skill_02;  //Skill.tbl�� id ����
        public int skill_03;  //Skill.tbl�� id ����
        public int skill_04;  //Skill.tbl�� id ����
        public int skill_05;  //Skill.tbl�� id ����
        public int skill_06;  //Skill.tbl�� id ����
        public int skill_07;  //Skill.tbl�� id ����
        public int skill_08;  //Skill.tbl�� id ����
    }

    public class ExpData
    {
        public int lv;  //���� ����
        public int curExp;  //���� ������ �������ϱ� ���� �ʿ��� ����ġ��
    }

    public class StageData
    {
        public int id;  //id
        public int waveId_01;  //Wave.tbl�� id ����
        public int waveId_02;  //Wave.tbl�� id ����
        public int waveId_03;  //Wave.tbl�� id ����
        public int waveId_04;  //Wave.tbl�� id ����
        public int waveId_05;  //Wave.tbl�� id ����
        public int waveId_06;  //Wave.tbl�� id ����
    }

    public class WaveData
    {
        public int id;  //id
        public int waveType;  //���̺� ���� : 1 = �Ϲ�, 2 = �߰� ������, 3 = ������
        public int monId_01;  //�Ϲ� ���� ���� : �߰� ������������ ������ �Ϸ��� ��. Monster.tbl�� id ����
        public int monId_02;  //Monster.tbl�� id ����
        public int monId_03;  //Monster.tbl�� id ����
        public int monId_04;  //Monster.tbl�� id ����
        public int monId_05;  //Monster.tbl�� id ����
        public int monId_06;  //Monster.tbl�� id ����
        public int specialMonId_01;  //waveType�� 1�� �ƴ� �� ���. 2�� ��� �߰� ������ ID �Է�. 3�� ��� ���� ID �Է�
        public int specialMonId_02;  //waveType�� 1�� �ƴ� �� ���. 2�� ��� �߰� ������ ID �Է�. 3�� ��� ���� ID �Է�
        public int spawn_01;  //Spawn.tbl�� id ����
        public int spawn_02;  //Spawn.tbl�� id ����
        public int spawn_03;  //Spawn.tbl�� id ����
        public int spawn_04;  //Spawn.tbl�� id ����
    }

    public class SpawnData
    {
        public int id;  //id
        public int monNum_01;  //�ٰŸ� ���� �� ���� ��
        public int monNum_02;  //���Ÿ� ���� �� ���� ��
        public int monNum_03;  //������ ���� �� ���� ��
        public int monNum_04;  //����Ʈ ���� �� ���� ��
        public int monNum_05;  //���� ���� �� ���� ��
    }

    public class RiskData
    {
        public int id;  //id
        public string name;  //����ũ ��Ī
        public float curHp;  //���� ü�� ���� ����
        public float maxHp;  //�ִ� ü�� ���� ����
        public float atkPwr;  //���ݷ� ���� ����
        public float moveSpeed;  //�̵� �ӵ� ���� ����
        public float criPrb;  //ġ��Ÿ Ȯ�� ���� ����
        public float criDmg;  //ġ��Ÿ ������ ���� ����
        public float gotDmg;  //�޴� ���ط� ���� ����
        public float hpRcv;  //�ʴ� ü�� ȸ���� ���� ����
        public float cdnPct;  //��ų ��Ÿ�� ������ ���� ����
        public int rmnRnd;  //���ӵǴ� ����ũ ����
        public float wonScale;  //��ȭ ����
    }

    public class MonsterData
    {
        public int id;  //id
        public string name;  //���� ��Ī
        public int monType;  //���� Ÿ��. 1=�ٰŸ�, 2=���Ÿ�, 3=������, 4=����Ʈ, 5=����
        public int projectile_01;  //������ ���ݿ� ���Ǵ� ����ü. Projectile.tbl���� id ����
        public int projectile_02;  //������ ���ݿ� ���Ǵ� ����ü. Projectile.tbl���� id ����
        public float giveWon;  //���͸� �׿��� �� �÷��̾ ȹ���ϴ� ��ȭ��
        public float giveExp;  //���͸� �׿��� �� �÷��̾ ȹ���ϴ� ����ġ��
        public float MaxHp;  //���� ���� �� �ִ� ü��
        public float atkPower;  //������ ���ݷ�
        public float moveSpeed;  //������ �̵� �ӵ�
        public int useSkill_01;  //���Ͱ� ����ϴ� ��ų. Skill.tbl���� id ����
        public int useSkill_02;  //���Ͱ� ����ϴ� ��ų. Skill.tbl���� id ����
        public int useSkill_03;  //���Ͱ� ����ϴ� ��ų. Skill.tbl���� id ����
        public int useSkill_04;  //���Ͱ� ����ϴ� ��ų. Skill.tbl���� id ����
        public int useSkill_05;  //���Ͱ� ����ϴ� ��ų. Skill.tbl���� id ����
        public int useSkill_06;  //���Ͱ� ����ϴ� ��ų. Skill.tbl���� id ����
        public int useSkill_07;  //���Ͱ� ����ϴ� ��ų. Skill.tbl���� id ����
        public int useSkill_08;  //���Ͱ� ����ϴ� ��ų. Skill.tbl���� id ����
    }

    public class ProjectileData
    {
        public int id;  //id
        public string name;  //����ü ��Ī
    }

    public class SkillData
    {
        public int id;  //id
        public string skillName;  //��ų ��Ī
        public int skillEffect_01;  //Effect.tbl�� id ����
        public int skillEffect_02;  //Effect.tbl�� id ����
        public float stiffTime;  //skillEffect=1�� �� ���. ���� ���� �ð�
        public float stunTime;  //skillEffect=2�� �� ���. ���� ���� �ð�
        public float dotTime;  //skillEffect=4�� �� ���. Dot ���� �ð�
        public float dotPeriod;  //skillEffect=4�� �� ���. Dot ó�� �ֱ�
        public float dotDmgPct;  //skillEffect=4�� �� ���. 1ƽ �� ������
        public float slowTime;  //skillEffect=5�� �� ���. ���ο� ���� �ð�
        public float slowPct;  //skillEffect=5�� �� ���. ���ο� �ۼ�Ʈ
        public float skillCool;  //��ų ��Ÿ��
        public float skillTime;  //��ų ���� �ð�
        public float dmgPct_01;  //��ų ������ �ۼ�Ʈ
        public float dmgPct_02;  //��ų ������ �ۼ�Ʈ. ���� Ÿ���� ���� � ���
        public float dmgPct_03;  //��ų ������ �ۼ�Ʈ. ���� Ÿ���� ���� � ���
        public float dmgPct_04;  //��ų ������ �ۼ�Ʈ. ���� Ÿ���� ���� � ���
        public float dmgPct_05;  //��ų ������ �ۼ�Ʈ. ���� Ÿ���� ���� � ���
    }

    public class EffectData
    {
        public int id;
        public string effectName;
    }
}
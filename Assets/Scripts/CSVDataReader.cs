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

    
public class CSVDataReader : Singleton<CSVDataReader>//싱글톤을 상속 받는다. 기본적으로는 MonoBehaviour(모노비헤이비어)가 달려있음
{
    

    #region Data Tables
    [Header("스텟 테이블 넣는 곳")]
    public TextAsset StUPtbl;
    [Header("레벨 테이블 넣는 곳")]
    public TextAsset LvStUptbl;
    [Header("클래스 테이블 넣는 곳")]
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
    public Dictionary<int, StUPData> stUPData = new Dictionary<int, StUPData>();//사실상 필요없음
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
            //연산 (이전꺼 -1)
            _id--;//1 > 처리를 안함
        }
        else
        {
            //연산 (이전꺼 +1)
            _id++;// 4 < 처리를 안함
        }

        if (_id == dataCnt + 1)//추가적인 if문으로 위에 안되어있던 요소를 처리
            _id = 1;
        else if (_id == 0)
            _id = dataCnt;
 
        PlayerPrefs.SetInt("charId", _id);

        curCharId = _id;

        return curCharId;
    }

    IEnumerator ChangeScene(string _scene)
    {
        while (true) //참 거짓 조건 반복문, (참:계속 반복, 거짓:탈출)
        { 
            yield return new WaitForSeconds(3);//리소스 로드가 다 되었는지 확인 후 참일 때 이 이후의 명령들을 실행함
            Debug.LogWarning("dd");
            break;
        }
      
        SceneManager.LoadScene( _scene );
    }

    
    public void MoveScene(SceneName sceneName)
    {
        string scene = "";
        // 여기에 메인으로 이동해라 하는부분
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
        public int startLv;  //강화 시작 레벨
        public float upPrb;  //성공 확률
        public int useWon;  //강화 시도 비용
        public float upMaxHp;  //최대 체력 상승량
        public float upAtkPwr;  //공격력 상승량
        public float upCriPrb;  //치명타 확률 상승량
        public float upCriDmg;  //치명타 데미지 상승량
        public float upHpRcv;  //초당 체력 회복량 상승량
        public float upCdnPct;  //스킬 쿨타임 감소율 상승량

    }

    public class LvStUpData
    {
        public float maxHp;  //레벨업 시 증가하는 최대 체력 값
        public float atkPwr;  //레벨업 시 증가하는 공격력 값
        public float criPrb;  //5레벨부터 적용, 10레벨 증가할 때 증가하는 치명타 확률 값
        public float criDmg;  //5레벨부터 적용, 10레벨 증가할 때 증가하는 치명타 데미지 값
        public float hpRcv;  //레벨업 시 증가하는 초당 체력 회복량 값
        public float cdnPct;  //5레벨부터 적용, 10레벨 증가할 때 증가하는 스킬 쿨타임 감소율 값
    }

    public class ClassData
    {
        public int id;  //id
        public string className;  //클래스 이름
        public float maxHp;  //생성 시 최대 체력
        public float atkPwr;  //생성 시 공격력
        public float criPrb;  //생성 시 치명타 확률
        public float criDmg;  //생성 시 치명타 데미지
        public float hpRcv;  //생성 시 초당 체력 회복량
        public float cdnPct;  //생성 시 스킬 쿨타임 감소율
        public float gotDmg;  //생성 시 받는 피해량
        public float moveSpeed;  //생성 시 이동 속도
        public int skill_01;  //Skill.tbl의 id 참조
        public int skill_02;  //Skill.tbl의 id 참조
        public int skill_03;  //Skill.tbl의 id 참조
        public int skill_04;  //Skill.tbl의 id 참조
        public int skill_05;  //Skill.tbl의 id 참조
        public int skill_06;  //Skill.tbl의 id 참조
        public int skill_07;  //Skill.tbl의 id 참조
        public int skill_08;  //Skill.tbl의 id 참조
    }

    public class ExpData
    {
        public int lv;  //현재 레벨
        public int curExp;  //다음 레벨로 레벨업하기 위해 필요한 경험치량
    }

    public class StageData
    {
        public int id;  //id
        public int waveId_01;  //Wave.tbl의 id 참조
        public int waveId_02;  //Wave.tbl의 id 참조
        public int waveId_03;  //Wave.tbl의 id 참조
        public int waveId_04;  //Wave.tbl의 id 참조
        public int waveId_05;  //Wave.tbl의 id 참조
        public int waveId_06;  //Wave.tbl의 id 참조
    }

    public class WaveData
    {
        public int id;  //id
        public int waveType;  //웨이브 종류 : 1 = 일반, 2 = 중간 보스전, 3 = 보스전
        public int monId_01;  //일반 몬스터 종류 : 중간 보스전에서도 나오게 하려고 함. Monster.tbl의 id 참조
        public int monId_02;  //Monster.tbl의 id 참조
        public int monId_03;  //Monster.tbl의 id 참조
        public int monId_04;  //Monster.tbl의 id 참조
        public int monId_05;  //Monster.tbl의 id 참조
        public int monId_06;  //Monster.tbl의 id 참조
        public int specialMonId_01;  //waveType이 1이 아닐 때 사용. 2일 경우 중간 보스의 ID 입력. 3일 경우 보스 ID 입력
        public int specialMonId_02;  //waveType이 1이 아닐 때 사용. 2일 경우 중간 보스의 ID 입력. 3일 경우 보스 ID 입력
        public int spawn_01;  //Spawn.tbl의 id 참조
        public int spawn_02;  //Spawn.tbl의 id 참조
        public int spawn_03;  //Spawn.tbl의 id 참조
        public int spawn_04;  //Spawn.tbl의 id 참조
    }

    public class SpawnData
    {
        public int id;  //id
        public int monNum_01;  //근거리 몬스터 총 생성 수
        public int monNum_02;  //원거리 몬스터 총 생성 수
        public int monNum_03;  //구조물 몬스터 총 생성 수
        public int monNum_04;  //엘리트 몬스터 총 생성 수
        public int monNum_05;  //보스 몬스터 총 생성 수
    }

    public class RiskData
    {
        public int id;  //id
        public string name;  //리스크 명칭
        public float curHp;  //현재 체력 영향 배율
        public float maxHp;  //최대 체력 영향 배율
        public float atkPwr;  //공격력 영향 배율
        public float moveSpeed;  //이동 속도 영향 배율
        public float criPrb;  //치명타 확률 영향 배율
        public float criDmg;  //치명타 데미지 영향 배율
        public float gotDmg;  //받는 피해량 영향 배율
        public float hpRcv;  //초당 체력 회복량 영향 배율
        public float cdnPct;  //스킬 쿨타임 감소율 영향 배율
        public int rmnRnd;  //지속되는 리스크 여부
        public float wonScale;  //재화 배율
    }

    public class MonsterData
    {
        public int id;  //id
        public string name;  //몬스터 명칭
        public int monType;  //몬스터 타입. 1=근거리, 2=원거리, 3=구조물, 4=엘리트, 5=보스
        public int projectile_01;  //몬스터의 공격에 사용되는 투사체. Projectile.tbl에서 id 참조
        public int projectile_02;  //몬스터의 공격에 사용되는 투사체. Projectile.tbl에서 id 참조
        public float giveWon;  //몬스터를 죽였을 때 플레이어가 획득하는 재화량
        public float giveExp;  //몬스터를 죽였을 때 플레이어가 획득하는 경험치량
        public float MaxHp;  //몬스터 생성 시 최대 체력
        public float atkPower;  //몬스터의 공격력
        public float moveSpeed;  //몬스터의 이동 속도
        public int useSkill_01;  //몬스터가 사용하는 스킬. Skill.tbl에서 id 참조
        public int useSkill_02;  //몬스터가 사용하는 스킬. Skill.tbl에서 id 참조
        public int useSkill_03;  //몬스터가 사용하는 스킬. Skill.tbl에서 id 참조
        public int useSkill_04;  //몬스터가 사용하는 스킬. Skill.tbl에서 id 참조
        public int useSkill_05;  //몬스터가 사용하는 스킬. Skill.tbl에서 id 참조
        public int useSkill_06;  //몬스터가 사용하는 스킬. Skill.tbl에서 id 참조
        public int useSkill_07;  //몬스터가 사용하는 스킬. Skill.tbl에서 id 참조
        public int useSkill_08;  //몬스터가 사용하는 스킬. Skill.tbl에서 id 참조
    }

    public class ProjectileData
    {
        public int id;  //id
        public string name;  //투사체 명칭
    }

    public class SkillData
    {
        public int id;  //id
        public string skillName;  //스킬 명칭
        public int skillEffect_01;  //Effect.tbl의 id 참조
        public int skillEffect_02;  //Effect.tbl의 id 참조
        public float stiffTime;  //skillEffect=1일 때 사용. 경직 적용 시간
        public float stunTime;  //skillEffect=2일 때 사용. 스턴 적용 시간
        public float dotTime;  //skillEffect=4일 때 사용. Dot 적용 시간
        public float dotPeriod;  //skillEffect=4일 때 사용. Dot 처리 주기
        public float dotDmgPct;  //skillEffect=4일 때 사용. 1틱 당 데미지
        public float slowTime;  //skillEffect=5일 때 사용. 슬로우 적용 시간
        public float slowPct;  //skillEffect=5일 때 사용. 슬로우 퍼센트
        public float skillCool;  //스킬 쿨타임
        public float skillTime;  //스킬 시전 시간
        public float dmgPct_01;  //스킬 데미지 퍼센트
        public float dmgPct_02;  //스킬 데미지 퍼센트. 여러 타수의 공격 등에 사용
        public float dmgPct_03;  //스킬 데미지 퍼센트. 여러 타수의 공격 등에 사용
        public float dmgPct_04;  //스킬 데미지 퍼센트. 여러 타수의 공격 등에 사용
        public float dmgPct_05;  //스킬 데미지 퍼센트. 여러 타수의 공격 등에 사용
    }

    public class EffectData
    {
        public int id;
        public string effectName;
    }
}
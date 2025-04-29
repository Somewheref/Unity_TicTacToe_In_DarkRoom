using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio.Language
{
    public class KS_RandomNameGenerator : MonoBehaviour
    {
        private static string[] prefixes = {
        "Alder", "Amber", "Archer", "Asher", "Aspen", "Aster", "August", "Aurora", "Autumn", "Avalon",
        "Basil", "Bay", "Beacon", "Bear", "Birch", "Bishop", "Blaise", "Blythe", "Bramble", "Briar",
        "Brook", "Cadence", "Calla", "Cedar", "Clay", "Clement", "Clover", "Coral", "Crane", "Cypress",
        "Dahlia", "Daisy", "Darcy", "Dove", "Eden", "Elm", "Ember", "Fable", "Fawn", "Felix",
        "Fern", "Finn", "Flint", "Forest", "Fox", "Gale", "Garnet", "Glen", "Haven", "Hazel",
        "Heath", "Holly", "Iris", "Ivy", "Jasper", "Jet", "Juniper", "Kestrel", "Lark", "Laurel",
        "Lavender", "Leaf", "Leif", "Lilac", "Linden", "Lotus", "Lyric", "Magnolia", "Maple", "Marigold",
        "Meadow", "Moss", "Nectar", "Nestor", "North", "Olive", "Opal", "Orchid", "Paisley", "Palmer",
        "Pansy", "Pearl", "Peony", "Peregrine", "Petal", "Phoenix", "Piper", "Plum", "Poppy", "Primrose",
        "Quill", "Quince", "Raven", "Reed", "River", "Robin", "Rose", "Rowan", "Sage", "Sapphire",
        "Scarlet", "Sequoia", "Sky", "Slate", "Sorrel", "Sparrow", "Starling", "Sterling", "Storm", "Summer",
            "Astro", "Cyber", "Galaxy", "Space", "Star", "Cosmo", "Alien", "Nova", "Lunar", "Orbit"
        };

        private static string[] infixes = {
        "an", "el", "in", "ar", "ra", "en", "al", "on", "um", "is",
        "or", "as", "us", "ir", "os", "il", "un", "at", "et", "ia",
        "es", "io", "am", "ur", "ix", "ob", "ul", "ir", "ot", "iv"
        };

        private static string[] suffixes = {
        "wood", "stone", "river", "leaf", "wind", "fire", "heart", "song", "sky", "field",
        "crest", "vale", "thorn", "shade", "light", "glen", "brook", "hill", "dale", "ridge",
        "bank", "cliff", "spring", "grove", "cove", "marsh", "peak", "path", "hollow", "strand",
        "ford", "brink", "bend", "haven", "wold", "mead", "fern", "mire", "stream", "wood",
        "glade", "thicket", "weald", "heath", "firth", "beck", "quill", "hurst", "shaw", "dell",
        "bourne", "garth", "fen", "ness", "lyn", "mere", "stead", "leigh", "wade", "vane",
        "ley", "moor", "down", "tarn", "stow", "coomb", "dean", "fleet", "warp", "byre",
        "croft", "holm", "keld", "tun", "wick", "bourne", "burn", "den", "ey", "ham",
        "kin", "stead", "thwaite", "toft", "worth", "acre", "barton", "bourne", "cot", "eyot",
        "hay", "ing", "ness", "royd", "set", "tow", "hurst", "ford", "land", "ridge"
        };


        private static string[] cn_prefixes = {
            "愤怒的", "快乐的", "神秘的", "奇幻的", "神奇的", "史诗的", "传奇的", "可爱的", "勇敢的", "冒险的",
            "宁静的", "梦幻的", "疯狂的", "狂野的", "天才", "机智的", "智慧的", "古怪的", "好奇的", "勤劳的",
            "光辉的", "黑暗的", "热情的", "冰冷的", "迷人的", "优雅的", "勇敢的", "神勇的", "坚强的", "聪明的",
            "狡猾的", "无畏的", "神圣的", "邪恶的", "富有的", "贫穷的", "强大的", "虚弱的", "善良的", "邪恶的",
            "普通的", "不凡的", "古老的", "年轻的", "迅速的", "缓慢的", "活泼的", "沉闷的", "快乐的", "悲伤的",
            "愤怒的", "平静的", "热烈的", "冷漠的", "激动的", "冷静的", "疯狂的", "理智的", "勇敢的", "胆小的",
            "英勇的",  "强壮的", "富裕的", "贫穷的", "高贵的", "光明的", "黑暗的",
            "光荣的", "羞辱的", "美丽的", "强硬的", "柔弱的", "聪明的", "乐观的", "悲观的",
            "正直的", "狡诈的", "勤奋的", "懒惰的", "诚实的", "虚伪的", "开放的"
        };

        private static string[] cn_infixes = {
        "小", "大", "超级", "超", "极", "奇", "幻", "神", "秘", "狂",
        "梦", "迷", "快乐", "幸福", "疯狂", "独特", "神奇", "奇异", "古怪", "未知",
        "古老", "年轻", "迅速", "缓慢", "强大", "弱小", "美丽", "丑陋", "智慧", "愚蠢",
        "勇敢", "胆小", "善良", "邪恶", "光明", "黑暗", "热情", "冷漠", "忠诚", "背叛",
        "平静", "狂暴", "稳重", "轻浮", "传统", "现代", "古典", "前卫", "乐观", "悲观",
        "理性", "感性", "活泼", "沉闷", "丰富", "贫瘠", "和平", "战争", "正义", "邪恶",
        "幽默", "严肃", "轻松", "紧张", "友好", "敌对", "真实", "虚幻", "明智", "愚笨",
        "开放", "封闭", "自由", "限制", "清晰", "模糊", "简单", "复杂", "宁静", "喧嚣",
        "纯净", "污染", "新鲜", "陈旧", "典雅", "粗俗", "严格", "宽松", "精确", "模糊",
        "硬朗", "柔软", "温暖", "寒冷", "湿润", "干燥", "明亮", "昏暗", "轻盈", "沉重"
    };

        private static string[] cn_suffixes = {
        "鸟", "猫", "狗", "鱼", "龙", "天空", "森林", "海洋", "城市", "山脉",
        "岛屿", "星星", "月亮", "太阳", "宇宙", "骑士", "战士", "法师", "精灵", "巨人",
        "王国", "历险", "梦境", "水晶", "宝藏", "迷宫", "花园", "瀑布", "沙漠", "绿洲",
        "山谷", "火山", "冰川", "传说", "探险", "远征", "寻宝", "征服", "奇迹", "彩虹",
        "隐士", "术士", "使者", "战场", "航海", "风暴", "猎人", "游侠", "歌者", "舞者",
        "魔法", "秘境", "传奇", "冠军", "勇者", "领主", "君主", "帝国", "盟友", "异界",
        "诗歌", "旋律", "节奏", "音符", "乐园", "梦幻", "夜晚", "黎明", "黄昏", "白昼",
        "幽灵", "怪兽", "巫师", "天使", "恶魔", "英雄", "冒险者", "流浪者", "探险家", "发现者",
        "机器人", "未来", "时光", "时空", "旅者", "守护者", "指挥官", "统治者", "领航员", "探索者",
        "奇幻", "奥秘","苹果",
    "橙子",
    "香蕉",
    "草莓",
    "葡萄",
    "樱桃",
    "梨",
    "蓝莓",
    "柠檬",
    "芒果",
    "桃子",
    "杏子",
    "猕猴桃",
    "菠萝",
    "火龙果",
    "西瓜",
    "哈密瓜",
    "石榴",
    "柿子",
    "椰子",
    "李子",
    "黑莓",
    "覆盆子",
    "无花果",
    "柚子",
    "蜜桔",
    "枇杷",
    "甜瓜",
    "木瓜",
    "百香果","苹果",
    "橙子",
    "香蕉",
    "草莓",
    "葡萄",
    "樱桃",
    "梨",
    "蓝莓",
    "柠檬",
    "芒果",
    "桃子",
    "杏子",
    "猕猴桃",
    "菠萝",
    "火龙果",
    "西瓜",
    "哈密瓜",
    "石榴",
    "柿子",
    "椰子",
    "李子",
    "黑莓",
    "覆盆子",
    "无花果",
    "柚子",
    "蜜桔",
    "枇杷",
    "甜瓜",
    "木瓜",
    "百香果","苹果",
    "橙子",
    "香蕉",
    "草莓",
    "葡萄",
    "樱桃",
    "梨",
    "蓝莓",
    "柠檬",
    "芒果",
    "桃子",
    "杏子",
    "猕猴桃",
    "菠萝",
    "火龙果",
    "西瓜",
    "哈密瓜",
    "石榴",
    "柿子",
    "椰子",
    "李子",
    "黑莓",
    "覆盆子",
    "无花果",
    "柚子",
    "蜜桔",
    "枇杷",
    "甜瓜",
    "木瓜",
    "百香果","苹果",
    "橙子",
    "香蕉",
    "草莓",
    "葡萄",
    "樱桃",
    "梨",
    "蓝莓",
    "柠檬",
    "芒果",
    "桃子",
    "杏子",
    "猕猴桃",
    "菠萝",
    "火龙果",
    "西瓜",
    "哈密瓜",
    "石榴",
    "柿子",
    "椰子",
    "李子",
    "黑莓",
    "覆盆子",
    "无花果",
    "柚子",
    "蜜桔",
    "枇杷",
    "甜瓜",
    "木瓜",
    "百香果","苹果",
    "橙子",
    "香蕉",
    "草莓",
    "葡萄",
    "樱桃",
    "梨",
    "蓝莓",
    "柠檬",
    "芒果",
    "桃子",
    "杏子",
    "猕猴桃",
    "菠萝",
    "火龙果",
    "西瓜",
    "哈密瓜",
    "石榴",
    "柿子",
    "椰子",
    "李子",
    "黑莓",
    "覆盆子",
    "无花果",
    "柚子",
    "蜜桔",
    "枇杷",
    "甜瓜",
    "木瓜",
    "百香果" };

        private static System.Random random = new System.Random();

        // Generate a random name
        [ContextMenu("Generate Name")]
        public void TestGenerate()
        {
            for(int i = 0; i < 20; i++)
            {
                Debug.Log(GenerateName());
            }
        }

        [ContextMenu("Generate ch Name")]
        public void TestGenerateCH()
        {
            for (int i = 0; i < 20; i++)
            {
                Debug.Log(GenerateChineseName());
            }
        }

        public static string GenerateName()
        {
            string lang = "en";
            try
            {
                lang = KS_Language.Instance.GetCurrentData().languageName;
            }
            catch
            {
                lang = "en";
            }
            if (lang == "en")
            {
                return GenerateEnglishName();
            }
            // added by Stanly: chinese name
            else if (lang == "ch")
            {
                return GenerateChineseName();
            }
            else
            {
                float randomValue = Random.Range(0.0f, 1.0f);
                if (randomValue <= 0.2f)
                {
                    return GenerateEnglishName();
                }
                else
                {
                    return GenerateChineseName();
                }
            }
        }

        public static string GenerateChineseName()
        {
            // TODO: Requires fixing

            string prefix = cn_prefixes[random.Next(cn_prefixes.Length)];
            string infix = cn_infixes[random.Next(cn_infixes.Length)];
            string suffix = cn_suffixes[random.Next(cn_suffixes.Length)];
            float randomValue = Random.Range(0.0f, 1.0f);
            if (randomValue <= 0.05f)
            {
                return prefix + infix + suffix;
            }
            else
            {
                return prefix + suffix;
            }


            //// patch fix by Stanly
            //string chName = "玩家" + Random.Range(1000, 9999);
            //return chName;
        }

        public static string GenerateEnglishName()
        {
            string prefix = prefixes[random.Next(prefixes.Length)];
            string infix = infixes[random.Next(infixes.Length)];
            string suffix = suffixes[random.Next(suffixes.Length)];
            float randomValue = Random.Range(0.0f, 1.0f);
            if (randomValue <= 0.2f)
            {
                return prefix + infix + suffix;
            }
            else
            {
                return prefix + suffix;
            }
        }
    }
}
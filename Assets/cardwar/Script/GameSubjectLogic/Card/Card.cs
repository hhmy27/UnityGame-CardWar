
public class Card
{
    /// <summary>
    /// 卡牌底图
    /// </summary>
    public int ImageIndex = 0;
    public int Demage = 0;

    public enum CardType { AtkCard, SkillCard, EventCard };    //定义三种类型卡牌
    public CardType WhichCard;  //判断当前何种卡牌
    public string CardTitle = "";
    public string CardIntro = "";
    //设计事件牌属性
    public int SmallImagie;
    //用一个ID来设计卡牌的功能（抽象事件牌技能）
    public int CardID;

    public Card(CardType type, string CardTitle, string CardIntro, int Demage, int CardID)
    {


        this.Demage = Demage;
        this.CardTitle = CardTitle;
        this.CardIntro = CardIntro;
        this.WhichCard = type;
        this.CardID = CardID;
        if (type == CardType.AtkCard)
        {

            ImageIndex = 0;
        }
        if (type == CardType.SkillCard)
        {

            ImageIndex = 1;
        }
        if (type == CardType.EventCard)
        {
            ImageIndex = 2;
        }

    }


}

namespace Challenges.Challenge.Y2023.Day07;

[ChallengeName("Day 7: Camel Cards")]
public class Day07 : IChallenge
{
    public async Task<object?> TaskPartOne(string input) => Solve(input, false);

    public async Task<object?> TaskPartTwo(string input) => Solve(input, true);


    private int Solve(string input, bool joker)
    {
        var x = Parse(input, joker).OrderBy(x => x);

        int i = 0;
        return x.Sum(x => x.bid * ++i);
    }


    private IEnumerable<Hand> Parse(string input, bool joker)
    {
        return input.GetLines().Select(x => CreateHand(x, joker));
    }

    private Hand CreateHand(string input, bool joker)
    {
        var split = input.Split(" ");
        return new Hand(split[0].ToCharArray().Select(x => ParseCardType(x, joker)).ToArray(), int.Parse(split[1]));
    }

    private record class Hand(CardType[] cards, int bid) : IComparable<Hand>, IComparable
    {
        public IEnumerable<HandCount> Groups => cards.GroupBy(s => s).Select(s => new HandCount(s.Key, s.Count())).OrderByDescending(x => x.count);

        public IEnumerable<HandCount> JokerGroups = null;

        public HandType Type ()
        {
            var group = Groups;
            if (cards.Any(x => x == CardType.Joker) && cards.Any(x => x != CardType.Joker))
            {
                if (JokerGroups == null)
                {
                    List<Hand> jokerHands = [];
                    jokerHands.AddRange(Groups.Where(x => x.card != CardType.Joker).Select(jokerHandToCreate => new Hand(cards.Select(x => CardReplacer(x, jokerHandToCreate.card)).ToArray(), bid)));

                    JokerGroups = jokerHands.MaxBy(x => x).Groups;
                }

                group = JokerGroups;
            }

                
            if (group.Any(x => x.count == 5)) return HandType.FiveOfKind;
            if (group.Any(x => x.count == 4)) return HandType.FourOfKind;
            if (group.Any(x => x.count == 3) && group.Any(x => x.count == 2)) return HandType.FullHouse;
            if (group.Any(x => x.count == 3)) return HandType.ThreeOfKind;
            if (group.Count(x => x.count == 2) == 2) return HandType.TwoPair;
            if (group.Any(x => x.count == 2)) return HandType.OnePair;
            return HandType.HighCard;
        }

        static CardType CardReplacer(CardType card, CardType replacer) => card == CardType.Joker ? replacer : card;

        public int CompareTo(Hand? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            if(Type() < other.Type()) return -1;
            if (Type() > other.Type()) return 1;


            for (var i = 0; i < cards.Length; i++)
            {
                if (cards[i] < other.cards[i]) return -1;
                if (cards[i] > other.cards[i]) return 1;
            }

            return 0;
        }

        public int CompareTo(object? obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is Hand other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Hand)}");
        }

        public record HandCount(CardType card, int count);
    }

    private CardType ParseCardType(char c, bool joker) =>
        (c, joker) switch
        {
            ('A', _) => CardType.Ace,
            ('K', _) => CardType.King,
            ('Q', _) => CardType.Queen,
            ('J', false) => CardType.Jack,
            ('J', true) => CardType.Joker,
            ('T', _) => CardType.Ten,
            ('9', _) => CardType.Nine,
            ('8', _) => CardType.Eight,
            ('7', _) => CardType.Seven,
            ('6', _) => CardType.Six,
            ('5', _) => CardType.Five,
            ('4', _) => CardType.Four,
            ('3', _) => CardType.Three,
            ('2', _) => CardType.Two,
            (_, _) => CardType.Joker
        };

    private enum HandType
    {
        FiveOfKind = 6,
        FourOfKind = 5,
        FullHouse = 4,
        ThreeOfKind = 3,
        TwoPair = 2,
        OnePair = 1,
        HighCard = 0
    }

    private enum CardType
    {
        Ace = 12,
        King = 11,
        Queen = 10,
        Jack = 9,
        Ten = 8,
        Nine = 7,
        Eight = 6,
        Seven = 5,
        Six = 4,
        Five = 3,
        Four = 2,
        Three = 1,
        Two = 0,
        Joker = -1
    }
}
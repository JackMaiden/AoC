namespace Challenges.Challenge.Y2023.Day04
{
    [ChallengeName("Day 4: Scratchcards ")]
    public class Day04: IChallenge
    {
        public async Task<object?> TaskPartOne(string input)
        {
            return GetCardScores(parseInput(input));
        }

        public async Task<object?> TaskPartTwo(string input)
        {
            var cards = parseInput(input).ToList();

            for (var i = 0; i < cards.Count(); i++)
            {
                var winners = cards[i].winningNumbers.Intersect(cards[i].cardNumbers).Count();

                var x = i + 1;
                for (var j = 0; j < winners; j++)
                {
                    cards[j+x].Copies += cards[i].Copies;
                }
            }

            return cards.Sum(x => x.Copies);
        }

        int GetCardScores(IEnumerable<Card> cards)
        {
            int score = 0;
            foreach (var card in cards)
            {
                var winners = card.winningNumbers.Intersect(card.cardNumbers).Count() -1;

                var x = 1 << winners;
                score += (1 << winners); //should be same as 2 power of x
            }

            return score;
        }


        IEnumerable<Card> parseInput(string input)
        {
            List<Card> cards = new();

            var lines = input.GetLines().Select(s => s.Split(":"));

            foreach (var line in lines)
            {
                var a = line[0].Split(" ").Where(s => !string.IsNullOrEmpty(s)).ToArray()[1];
                var id = int.Parse(a);

                var numberSections = line[1].Split("|").Select(s => s.Split(" ").Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToArray()).ToArray();

                cards.Add(new Card(id, numberSections[0], numberSections[1]));
            }

            return cards;
        }

        class Card
        {
            public Card(int id, IEnumerable<int> winningNumbers, IEnumerable<int> cardNumbers)
            {
                ID = id;
                this.winningNumbers = winningNumbers;
                this.cardNumbers = cardNumbers;
                this.Copies = 1;
            }

            public int ID { get; set; }

            public int Copies { get; set; }

            public IEnumerable<int> winningNumbers { get; set; }

            public IEnumerable<int> cardNumbers { get; set; }


        }


    }
}
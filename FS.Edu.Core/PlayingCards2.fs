module PlayingCards2

    type Suit =
        | Hearts
        | Diamonds
        | Spades
        | Clubs

    type Value = 
        | Two 
        | Three 
        | Four
        | Five
        | Six
        | Seven
        | Eight
        | Nine
        | Ten

        static member GetAllValues() = [
            yield Two
            yield Three
            yield Four
            yield Five
            yield Six
            yield Seven
            yield Eight
            yield Nine
            yield Ten           
        ]

    type Rank =
        | Value of Value
        | Ace
        | King
        | Queen
        | Jack

        static member GetAllRanks() = 
            [ yield Ace
              for v in Value.GetAllValues() do yield Value(v)
              yield Jack
              yield Queen
              yield King ]

    type Card = { Suit: Suit; Rank: Rank }

    let fullDeck = 
        [ for suit in [ Hearts; Diamonds; Clubs; Spades] do
              for rank in Rank.GetAllRanks() do 
                  yield { Suit=suit; Rank=rank } ]

    let card = { Suit=Diamonds; Rank=Value(Two)}
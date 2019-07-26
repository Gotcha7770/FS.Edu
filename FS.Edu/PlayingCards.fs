module PlayingCards

    type Suit =
        | Hearts
        | Diamonds
        | Spades
        | Clubs

    type Rank = 
        /// Represents the rank of cards 2 .. 10
        | Value of int
        | Ace
        | King
        | Queen
        | Jack

        static member GetAllRanks() = 
            [ yield Ace
              for i in 2 .. 10 do yield Value i
              yield Jack
              yield Queen
              yield King ]

    type Card = { Suit: Suit; Rank: Rank }

    let fullDeck = 
        [ for suit in [ Hearts; Diamonds; Clubs; Spades] do
              for rank in Rank.GetAllRanks() do 
                  yield { Suit=suit; Rank=rank } ]

    let card = { Suit=Diamonds; Rank=Value(100)}
// Learn more about F# at http://fsharp.org

open System

//свой оператор не равно
let (!=) x y = x <> y

//форма записи для таблицы истинности
let andTruthTable x y =
    match x, y with
    | true, true-> true
    | _, _ -> false

let orTruthTable x y =
    match x, y with
    | false, false -> false
    | _, _ -> true

let xorTruthTable x y =
    match x, y with
    | x, y when x != y -> true
    | _, _ -> false

// Выводит таблицу истинности для заданной функции
let printTruthTable f =
    printfn "      |  true | false |"
    printfn "      +-------+-------+"
    printfn " true |  %5b  |  %5b  |" (f true true) (f true false)
    printfn " false|  %5b  |  %5b  |" (f false true) (f false false)
    printfn "      +-------+-------+"
    printfn "\n"

// Размеченное объединение, представляющее масть карты
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

type BinaryTree =
    | Node of int * BinaryTree * BinaryTree
    | Empty

[<EntryPoint>]
let main argv =
    printTruthTable (&&)
    printTruthTable (||)
    0 // return an integer exit code

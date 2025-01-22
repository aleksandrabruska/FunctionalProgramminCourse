module BST
type BST<'a> =
  | Empty
  | Node of BST<'a> * 'a * BST<'a>

let rec contains (x: 'a) (n: BST<'a>) =
    match n with
        | Empty -> false
        | Node (left, curr, right) -> 
            if curr = x then true
            elif curr < x then (contains x left)
            else (contains x right)

let rec disp (n: BST<'a>) =
     match n with
        | Empty -> "Empty"
        | Node (left, curr, right) -> 
            let leftStr = disp left
            let rightStr = disp right
            (sprintf "Node (%A, %A, %A)" (leftStr) curr (rightStr))

let rec insert (x: 'a) (n: BST<'a>) =
    match n with 
        | Empty -> Node (Empty, x, Empty)
        | Node (left, curr, right) when curr > x ->
                let newleft = (insert x left)
                Node (newleft, curr, right)
        | Node (left, curr, right) when curr < x -> 
                let newright = (insert x right)
                Node (left, curr, newright)
        | _ -> n

(*
let rec remove (x: 'a) (n: BST<'a>) =
    match n with
        | Empty -> Empty
        | Node (left, curr, right) when curr > x ->
            let newleft = (remove x left)
            Node (newleft, curr, right)
        | Node (left, curr, right) when curr < x ->
            let newright = (remove x right)
            Node (newright, curr, right)
        | Node (Empty, _, Empty) ->
            Empty
        | Node (left, _, Empty) -> 
            left
        | Node (Empty, _, right) ->
            right
        | Node (left, _, right) ->
            let (Node(value', _, _)) = findInOrderPredecessor left
            let left' = delete value' left
            Node (value', left', right)


*)
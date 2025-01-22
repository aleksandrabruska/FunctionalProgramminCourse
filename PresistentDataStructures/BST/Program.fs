open BST

let tree = Node (
    Node(Empty, 1, Empty), 
    2,
    Node (Empty, 5, Empty))
printfn "%A" (contains 12 tree)
printfn "%A" (disp tree)
let tree2 = (insert 3 tree)
printfn "%A" (disp tree2)

with open('Tokens.txt', 'r') as file:
    i = -1
    for line in file:
        i += 1
        print(f'Assert.AreEqual(TokenKind.{line[:-2]}, tokens[{i}].Kind);')

# Dungeon Generator
Dungeon Generator que cria caminhos e salas utilizando **recursividade**.
## Ideias
- Criar um gerador de caminhos que liga o início (spawn) ao fim.
  - Expandir esse gerador de caminhos de forma a que **mude de direção** dependendo de um parâmetro (ou seja, definir com que frequência esse caminho muda de direção).
- Criar parâmetros que:
  - Guardam os **tipos de tiles** que devem ser usados para o chão, parede, etc...
  - Permitem definir a **complexidade da dungeon**, fazendo com que haja mais caminhos (labirinto)
  - **Guarda o player object** para, possívelmente, dar **spawn numa sala específica**
## Objetivo
Criar algo semelhante com a seguinte imagem:

![alt text](https://media.milanote.com/p/images/1JF6yS1p7Gd08o/5ff/imagem.png)

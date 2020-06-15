# Dungeon Generator
Dungeon Generator que cria caminhos e salas utilizando **recursividade**.

Vídeo demonstração: https://youtu.be/mRkX2v4JnKw
## Como usar?
1. Criar **dois** tilemaps: um para o **chão** e outro para as **paredes**

![alt text](https://media.milanote.com/p/images/1JKaqz1o5t530F/aXU/imagem.png)

2. Colocar o script **"DungeonGenerator"** no GameObject **"Grid"**

![alt text](https://media.milanote.com/p/images/1JKaw11o5t530I/dv8/imagem.png)

3. Definir os seguintes parâmetros (**Atenção: Na imagem estão retratados os parâmetros considerados ideais**)

![alt text](https://media.milanote.com/p/images/1JKati1o5t530G/QVB/imagem.png)

## Como funciona?
**Dungeon Generator** foi desenhado para criar dungeons simples (**usando recursividade**) com alguns parâmetros que permitem controlar de que maneira o gerador irá comportar.
- Primeiramente, o gerador desenha uma **sala** com *x entradas* na posição (0, 0). Essa sala tem um tamanho random que vai desde a largura da entrada (mais 2, porque as entradas não podem ser geradas nos "vértices"/"pilares" das salas) até ao tamanho máximo definido pelo utilizador.
- De seguida, são gerados os **corredores** (que são contítuidos por quadrados de tamanho equivalente à largura da entrada) dependendo do nº de entradas dessa sala. Os corredores têm um tamanho random entre o mínimo e o máximo definido pelo utilizador.
- No final do corredor, é gerado a **próxima sala** e repete-se o processo.

E assim vai, de forma recursiva, gerando as várias salas e os respetivos corredores.

## Problemas conhecidos
- Apesar dos corredores não ficarem uns em cima dos outros, o mesmo não acontece com as salas. Isto porque o algoritmo só deteta se, nos corredores, pode ser gerado outro quadrado ou não. Ele acaba por gerar a sala no fim do corredor. **Tentei resolver este problema, mas acabava por causar ainda mais problemas sérios ao gerador (a ponto de o *Unity* crashar)**

# Pré-produção
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

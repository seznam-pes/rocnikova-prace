# Ročníková práce
Ročníková práce za 2. ročník. Téma: hra inspirovaná českou hrou Polda. \
Hru vytvářím za pomocí jazyku C# a WPF.


## 📝 CHANGELOG:

* 17.04.2026 - vymyšlen základní příběh & zahájení práce na menu
* 20.04.2026 - komplet dodělané UI (mimo in-game tlačítek), zahájení práce na zvuku
* 26.04.2026 - přidána možnost upravovat velikost okna
* 01.05.2026 - save systém, redesign menu, přechody z menu
* 15.05.2026 - přidány obrázky + nějaký další věci idk už


## 🖼️ SCÉNY:
### Scéna 1:
📍 Vesnice - náměstí \
📍 Dům zmizelé dívky \
📍 Hospoda

📦 Dopis od vodníka \
📦 Klíč od domu rodiny zmizelé dívky *(v tom domě najde ten dopis, po přečtení se objeví matka, bude vystrašená že co dělá hráč v jejím domě. hráč jí vvysvětlí že je detektiv, pak mezi nimi začne dialog)*

🧍 Matka dívky \
🧍 Hospodský \
🧍 Stará paní na ulici *(NPC)* \
🧍 Random NPCs v hospodě

🧩 Dopis bude zamčený v sejfu, který bude zakódovaný. Kód bude napsaný na papírku vedle sejfu, ten bude však zašifrovaný skrze čísla (např. kód bude "ahoj", šifra bude "246665"). Dopis pak řekne hráči kudy jít.

---

### Scéna 2:
📍 Les \
📍 Mlýn \
📍 Rybník 1 *(vodníkův "soused")* \
📍 Rybník 2 *(real vodník)*

📦 Mapa lesa \
📦 Zvonek na vodníka *(návnada, dostane od pána v lese)*

🧍 Mlynář *(dá hráči mapu)*\
🧍 Vodníkův soused \
🧍 Starý pán v lese, sbírá houby

🧩 Mapa bude ve stylu "vždy běž za listnatými stromy, nikdy nechoď za nízkými jehličnany". Hráče pak bude čekat série křižovatek, kde bude muset dojít správnou cestou, aby došel k vodníkovi. V případě špatné cesty dojde k sousedovi vodníka.

---

### Scéna 3:

## 📔 PŘÍBĚH (Inspirace: Vodník od Erbena, detailní koncept pomocí AI):
### 🧩 ZÁKLADNÍ PREMISA

V malé české vesnici zmizí mladá dívka. Místní tvrdí, že „ji vzal vodník“, ale ty jako detektiv tomu samozřejmě nevěříš.
…aspoň první půl hodinu.

---

### 🧍‍♂️ HLAVNÍ POSTAVA

* cynický detektiv (klidně parodie na Pankráce)
* nevěří na nadpřirozeno
* suchý humor, komentuje absurdity vesnice

---

### 📖 STRUKTURA PŘÍBĚHU

#### 🟢 1. AKT – Příjezd do vesnice

* hráč přijíždí → hned divné věci:

  * starosta nechce „panikařit“
  * bába tvrdí, že „už si brousí hrníčky“
* první úkoly:

  * prohledat dům zmizelé
  * vyslechnout matku (emocionální + lehce přehnané dialogy)

👉 **twist:** všichni mluví o vodníkovi úplně vážně

---

#### 🟡 2. AKT – Vyšetřování

Lokace:

* rybník
* hospoda
* les
* mlýn

Zjištění:

* u rybníka podivné stopy
* někdo viděl „zeleného chlapa“
* v hospodě se objeví NPC, co tvrdí, že vodník dluží pivo 😄

Puzzle nápady:

* sestavení „důkazu“ (stopy, kus látky, hrníček)
* získání návnady → jak přivolat vodníka
* kombinace absurdních předmětů (typický Polda styl)

---

#### 🔵 3. AKT – Setkání s vodníkem

Vodník není čisté zlo:

* bydlí pod hladinou
* sbírá duše do hrníčků (ale bere to jako „sběratelskou vášeň“)
* má byrokratické problémy (např. „přeplněná evidence duší“)

👉 dialogy jsou klíč:

* můžeš ho vyslýchat
* nebo s ním „vyjednávat“

---

#### 🔴 4. AKT – Zvrat

Zjistíš:

* dívka není úplně oběť
* šla k vodníkovi dobrovolně (útěk od reality / vztah / nuda ve vesnici)

➡️ hráč musí rozhodnout:

* „zachránit ji“ násilím
* nebo pochopit situaci

---

#### ⚖️ 5. FINÁLE – Více konců

**1. Klasický (tragikomický):**

* přemůžeš vodníka
* dívka se vrátí
* ale není šťastná

**2. „Erbenovský“:**

* všechno skončí špatně
* duše skončí v hrníčku

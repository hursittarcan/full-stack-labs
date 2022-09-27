# H3 Lab Components
In deze lab bouwen en testen we onze eerste components. Angular is een component based framework. Component based werken wordt ook gebruikt in frameworks zoals React, VueJS, Flutter, ... Gegeven is deze repo. Hierin staat een Angular project met reeds een confessions klasse. **Navigeer naar deze folder via de CLI** en voer volgend commando uit: ```npm install```
 
Vervolgens voer je, nog steeds in deze folder, het commando ```ng serve -o``` uit. Hiermee zal de applicatie gestart worden en gaat er automatisch een browser open. Moest dit laatste niet het geval zijn, surf je naar http://localhost:4200. Bij elke aanpassing in de code zal de browser automatisch refreshen.

Components kunnen in principe perfect handmatig aangemaakt worden. Je kan de bestanden zelf aanmaken en de boilerplate code voorzien. In de praktijk wordt dit echter niet vaak gedaan. De AngularCLI voorziet commando's om onder andere components aan te maken.

![alt_text](https://i.imgur.com/TT9FcyW.png "image_tooltip") Bekijk de structuur van de reeds aangemaakte "app component". Uit welke bestanden bestaat deze component? Hoe wordt de component opgeroepen?


## Een nieuwe component maken
Open een terminal venster en voer onderstaand commando uit:
```
ng generate component confession-item
```

![alt_text](https://i.imgur.com/TT9FcyW.png "image_tooltip") Welke bestanden en mappen zijn net aangemaakt? Wat is er veranderd in de ```app.module.ts``` file? Bekijk de documentatie van het [ng generate](https://angular.io/cli/generate) commando

Om een component te kunnen gebruiken, moet hij altijd toegevoegd worden aan de 
declarations array binnen je app.module.ts file. De CLI doet dit 
automatisch. Als je components handmatig aanmaakt is deze stap belangrijk!

We passen de HTML van de component aan door de ```confession-item.component.html``` file aan te passen naar volgende inhoud:
```html
<div>
  <p class="post">onze eerste confession</p>
  <p class="author">Auteur: Dries</p>
  <p class="department">Departement: PXL-Digital</p>
</div>
```

![alt_text](https://i.imgur.com/TT9FcyW.png "image_tooltip") Zorg ervoor dat deze component een groene achtergrondkleur krijgt.

## Een component gebruiken
in de file ```confession-item.component.ts``` zie je dat de component een selector tag gekregen heeft, namelijk ```app-confession-item```. Elke Angular component heeft zijn eigen custom HTML selector die start met het *app* prefix. Deze selector kan je in je project gebruiken om die specifieke component op te roepen. 

Ga naar de file ```app-component.html``` en voeg onderstaande code toe en bekijk het resultaat in de browser:
```html
<app-confession-item></app-confession-item>
<app-confession-item></app-confession-item>
<app-confession-item></app-confession-item>
```

## Databinding & Lifecycle hooks
Momenteel steken we statische data rechtstreeks in de view van de component. Dit wordt in de praktijk niet gedaan. Vaak komt de data vanuit een API of dergelijke in de klasse van de component terecht. Dit zijn dan vaak properties van die component. Deze properties worden dan via databinding aan de view toegevoegd.

Voeg een Confession object toe als property van de component klasse in ```confession-item.component.ts``` als volgt:
```typescript
export class ConfessionItemComponent implements OnInit {
  confession: Confession | undefined;

```
Vervolgens initialiseren we dit object in de ```ngOnInit``` methode:
```typescript
  ngOnInit(): void {
    this.confession = new Confession("onze eerste confession", "PXL-Digital","Dries");
  }
```

tenslotte passen we de ```confession-item.component.html``` file aan zodat deze niet meer de statische data gebruikt, maar de data uit het object dat we net hebben aangemaakt:
```html
<div>
    <p class="post">{{confession?.post}}</p>
    <p class="author">{{confession?.author}}</p>
    <p class="department">{{confession?.department}}</p>
</div>

```
De view maakt nu gebruik van het object dat gedefinieerd werd in de component klasse (in de *ngOnInit* lifecycle hook). Databinding is iets waar we in het volgende hoofdstuk dieper op ingaan. Momenteel is dit voor elke instantie van de component hetzelfde object. In een later hoofdstuk zien we hoe we deze objecten dynamisch kunnen doorgeven.

![alt_text](https://i.imgur.com/TT9FcyW.png "image_tooltip") Voorzie zelf nog een component met als naam *add-confession* in de folder ```./src/app/components/```. Deze map components dien je eerst zelf nog aan te maken. Voeg deze component ook toe aan de view van de app component.

Meestal willen we op voorhand nadenken over de projectstructuur van je Angular applicatie. Bij grotere applicaties verlies je anders al snel het overzicht. Gebruik je Angular (of een ander component based framework) voor je IT project? Doe hier dan op voorhand het nodige opzoekingswerk!

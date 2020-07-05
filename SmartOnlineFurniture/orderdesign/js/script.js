var dragItem=document.getElementById("dragElement");
var dropLoc=document.getElementById("dropLocation");

dragItem.ondragstart=function(evt){
	evt.dataTransfer.setData('key',evt.target.id);
	console.log("It's Draging...");

}

dropLoc.ondragover=function(evt)
{
	evt.preventDefault();
	console.log("It's Drageover....");
}

dropLoc.ondrop=function(evt)
{
	
	var dropItem=evt.dataTransfer.getData('key');
	evt.preventDefault();
	console.log("it's Drop...");
	console.log(dropItem);
	var myElement=document.getElementById(dropItem);
	/*console.log(myElement);*/
	myNewElement=document.createElement('img');
	myNewElement.src=myElement.src
	dropLoc.appendChild(myNewElement);
}

var dragItem1=document.getElementById("dragElement1");
var dragItem2=document.getElementById("dragElement2");
var dragItem3=document.getElementById("dragElement3");
var dragItem4=document.getElementById("dragElement4");
var dragItem5=document.getElementById("dragElement5");
var dragItem6=document.getElementById("dragElement6");
var dragItem7=document.getElementById("dragElement7");
var dragItem8=document.getElementById("dragElement8");
var dragItem9=document.getElementById("dragElement9");


dragItem1.ondragstart=function(evt){
	evt.dataTransfer.setData('key',evt.target.id);
	console.log("It's Draging...");
}
dragItem2.ondragstart=function(evt){
	evt.dataTransfer.setData('key',evt.target.id);
	console.log("It's Draging...");
}
dragItem3.ondragstart=function(evt){
	evt.dataTransfer.setData('key',evt.target.id);
	console.log("It's Draging...");
}
dragItem4.ondragstart=function(evt){
	evt.dataTransfer.setData('key',evt.target.id);
	console.log("It's Draging...");
}
dragItem5.ondragstart=function(evt){
	evt.dataTransfer.setData('key',evt.target.id);
	console.log("It's Draging...");
}
dragItem6.ondragstart=function(evt){
	evt.dataTransfer.setData('key',evt.target.id);
	console.log("It's Draging...");
}
dragItem7.ondragstart=function(evt){
	evt.dataTransfer.setData('key',evt.target.id);
	console.log("It's Draging...");
}
dragItem8.ondragstart=function(evt){
	evt.dataTransfer.setData('key',evt.target.id);
	console.log("It's Draging...");
}
dragItem9.ondragstart=function(evt){
	evt.dataTransfer.setData('key',evt.target.id);
	console.log("It's Draging...");
}


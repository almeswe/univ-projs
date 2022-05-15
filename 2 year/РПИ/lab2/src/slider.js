var timer = 0;
var offset = 0;
const delay = 6000;
const keyName = "__lab2_slider_offset";

function sliderInitStorage() {
	let data = localStorage.getItem(keyName);
	if (data == null) {
		localStorage.setItem(keyName, 0);
	} 
	else {
		offset = parseInt(data);
	}
	console.log(offset);
}

function sliderUpdateStorage(data) {
	localStorage.setItem(keyName, data);
}

function sliderMove() {
	timer = setTimeout(function() {
		var polosa = document.getElementById('polosa');
		offset = offset - 1280;
		if (offset < -2560) {
			offset = 0;
			clearTimeout(timer);
		}
		polosa.style.offset = offset + 'px';
		sliderUpdateStorage(offset);
		sliderMove();
	}, delay);
}

function sliderStart() {
	sliderInitStorage();
	var polosa = document.getElementById('polosa');
	polosa.style.offset = offset + 'px';
	sliderMove();
}

sliderStart();
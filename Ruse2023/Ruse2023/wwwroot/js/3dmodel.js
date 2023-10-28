
// main.js
function threeExample(canvasId) { // NOTE: three dancing balls example

	var canvas = document.getElementById(canvasId);

	// NOTE: create the scene to place objects in
	var scene = new THREE.Scene();
	scene.background = new THREE.Color(0x6699cc); // NOTE: make the background blue for the sky
	scene.matrixWorldAutoUpdate = true;



	// NOTE: the width and height of the parent element; this information is static and should be updated when the browser window is resized
	var size = {
		width: canvas.parentNode.offsetWidth,
		height: canvas.parentNode.offsetHeight
	};

	// NOTE: issue these statements when resizing the window
	// camera.aspect = size.width / size.height;
	// camera.updateProjectionMatrix();
	// renderer.setPixelRatio(window.devicePixelRatio);
	// renderer.setSize(size.width, size.height);



	// NOTE: create the camera with 53 degree field of view; this is how the scene is viewed by the user
	var camera = new THREE.PerspectiveCamera(53, size.width / size.height, 1, 5000);

	// NOTE: position the camera in space a bit
	camera.position.z = 5;


	var renderer = new THREE.WebGLRenderer({
		canvas: canvas
	});
	renderer.shadowMap.enabled = true;
	renderer.setPixelRatio(window.devicePixelRatio);
	renderer.setSize(size.width, size.height);
	renderer.render(scene, camera);



	// NOTE: create three spheres
	var sphere1 = new THREE.Mesh(new THREE.SphereGeometry(0.75), new THREE.MeshLambertMaterial({ color: 0xff0000 }));
	sphere1.position.x = sphere1.position.y = 0;
	sphere1.position.z = -50;
	sphere1.receiveShadow = true;
	sphere1.castShadow = true;
	scene.add(sphere1);


	var sphere2 = new THREE.Mesh(new THREE.SphereGeometry(0.75), new THREE.MeshLambertMaterial({ color: 0x00ff00 }));
	sphere2.position.x = sphere2.position.y = 0.5;
	sphere2.position.z = -25;
	sphere2.receiveShadow = true;
	sphere2.castShadow = true;
	scene.add(sphere2);


	var sphere3 = new THREE.Mesh(new THREE.SphereGeometry(0.75), new THREE.MeshLambertMaterial({ color: 0x0000ff }));
	sphere3.position.x = sphere3.position.y = 0.5;
	sphere3.position.z = -25;
	sphere3.receiveShadow = true;
	sphere3.castShadow = true;
	scene.add(sphere3);



	// NOTE: create the ground
	var plane = new THREE.Mesh(new THREE.PlaneGeometry(1000, 1000), new THREE.MeshLambertMaterial({ color: 0x00aa00 }));
	plane.position.x = 0;
	plane.position.y = 0;
	plane.position.z = -50;
	plane.rotation.x = 4.75;
	plane.receiveShadow = true;
	scene.add(plane);



	// NOTE: this light will follow the mouse cursor
	var light = new THREE.PointLight(0xffffff);
	light.position.x = 51.5;
	light.position.y = 862.5;
	light.position.z = 10;
	light.castShadow = true;
	light.shadow.camera.top = 2500;
	light.shadow.camera.bottom = - 2500;
	light.shadow.camera.left = - 2500;
	light.shadow.camera.right = 2500;
	light.shadow.camera.near = 1;
	light.shadow.camera.far = 1000;
	light.shadow.mapSize.set(2048, 2048);

	scene.add(light);

	// NOTE: this function will set the light based on the mouse cursor
	var moveLight = function (event) {
		var offX = 0;
		var offY = 0;
		if (typeof window.pageXOffset != "undefined") {
			offX = window.pageXOffset;
			offY = window.pageYOffset;
		}
		else {
			if (document.documentElement.scrollTop == 0) {
				offX = document.body.scrollLeft;
				offY = document.body.scrollTop;
			}
			else {
				offX = document.documentElement.scrollLeft;
				offY = document.documentElement.scrollTop;
			}
		}
		var x, y;
		if (typeof event.pageX != "undefined") {
			x = event.pageX;
			y = event.pageY;
		}
		else {
			x = event.clientX;
			y = event.clientY;
		}
		x -= offX;
		y -= offY;
		if (x < 0) {
			x = 0;
		}
		if (y < 0) {
			y = 0;
		}

		light.position.x = x - size.width / 2;
		light.position.y = size.height / 2 - y;
	};

	var handler = function (element, type, func) {
		if (element.addEventListener) {
			element.addEventListener(type, func, false);
		} else if (window.attachEvent) {
			element.attachEvent("on" + type, func);
		} else {
			element["on" + type] = func;
		}
	};

	handler(canvas, "mousemove", moveLight);


	// NOTE: this will reset the light after the mouse moves out
	handler(canvas, "mouseout", function () {
		light.position.x = 51.5;
		light.position.y = 862.5;
		light.position.z = 10;
	});


	// NOTE: MUST HAVE AN ANIMATE FUNCTION
	var animate = function () {
		var time = Date.now();

		sphere1.position.x = Math.cos(time * (1.0 / 200) + 10.0);
		sphere1.position.y = Math.sin(time * (1.0 / (5 * 200)) + 10.0) + 5;
		sphere1.position.z = Math.sin(time * (1.0 / 200) + 10.0) - 10;

		sphere2.position.x = Math.cos(time * (1.0 / (5 * 200)) + 10.0) * 1.2;
		sphere2.position.y = Math.sin(time * (1.0 / 200) + 10.0) * 1.2 + 5;

		sphere3.position.x = Math.cos(time * (1.0 / (5 * 200)) + 10.0) * 1.5 + 6;
		sphere3.position.y = Math.sin(time * (1.0 / (5 * 200)) + 10.0) * 1.5 + 7;

		requestAnimationFrame(animate);
		renderer.render(scene, camera);
	};
	animate();

}
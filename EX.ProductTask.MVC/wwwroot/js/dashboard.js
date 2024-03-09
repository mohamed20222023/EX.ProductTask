

$.get({
	url: '/Chart/Chart',
	success: function (data) {
		console.log(data.map(i => i.label))
		const ctx = document.getElementById('myChart');
		new Chart(ctx, {
			type: 'bar',
			data: {
				labels: data.map(i => i.label),
				datasets: [{
					label: '# of Votes',
					data: [20, 30, 40, 50, 60, 70],
					borderWidth: 1
				}]
			},
			options: {
				scales: {
					y: {
						beginAtZero: true
					}
				}
			}
		});

	}
})


$.get({
	url: '/Chart/Chart',
	success: function (data) {
		console.log(data.map(i => i.label))
		const ctx = document.getElementById('alaaChart');
		new Chart(ctx, {
			type: 'pie',
			data: {
				labels: data.map(i => i.label),
				datasets: [{
					label: 'My First Dataset',
					data: [300, 50, 100],
					backgroundColor: [
						'rgb(255, 99, 132)',
						'rgb(54, 162, 235)',
						'rgb(255, 205, 86)'
					],
					hoverOffset: 4
				}]
			}
		});

	}
})



function generateExcel() {
	var table = document.getElementById("example");
	var data = [];

	for (var i = 0; i < table.rows.length; i++) {
		var row = [];

		for (var j = 0; j < table.rows[i].cells.length - 1; j++) { // Exclude the last cell
			row.push(table.rows[i].cells[j].textContent);
		}

		data.push(row);
	}

	var wb = XLSX.utils.book_new();
	var ws = XLSX.utils.aoa_to_sheet(data);
	XLSX.utils.book_append_sheet(wb, ws, "Dashboard Data");
	XLSX.writeFile(wb, "dashboard.xlsx");
}
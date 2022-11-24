

$(document).ready(function () {
    ShowAllProducts();
    function ShowAllProducts() {
        $.ajax({
            url: "https://localhost:7126/CooperativeRoomCooperative/getPond",
            type: "get",
          
            success: function (result, status, xhr) {
                var earning = document.getElementById('earning').getContext('2d');
                var myChart = new Chart(earning, {
                    type: 'bar',

                    data: {
                        labels: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
                        datasets: [{
                            label: 'Ao',
                            data: result,
                            backgroundColor: [

                                'rgba(54, 162, 235, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(54, 162, 235, 1)',


                            ],

                        }]
                    },
                    options: {
                        responsive: true
                    }
                });
            },
            error: function (xhr, status, error) {
                console.log("ssssssssssss")
            }
        });

    };
});

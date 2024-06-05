function deleteNews(url,returnPage) {    

    Swal.fire({
        title: 'آیا از حذف این آیتم مطمئن هستید؟',
        showCancelButton: true,
        confirmButtonText: 'حذف',
        showLoaderOnConfirm: true,
        customClass: {
            confirmButton: 'btn btn-danger me-3',
            cancelButton: 'btn btn-secondary'
        },
        preConfirm: ()=> {
            $.ajax({
                type: "Get",
                url: url,
                success: function () {
                    window.location = returnPage;
                },
                error: function () {
                    alert("خطایی رخ داده است!");
                }
            });
        },
        backdrop: true,
        allowOutsideClick: () => !Swal.isLoading()
    });

}

function deleteItem(url,returnPage) {

    Swal.fire({
        title: 'آیا از حذف این آیتم مطمئن هستید؟',
        showCancelButton: true,
        confirmButtonText: 'حذف',
        showLoaderOnConfirm: true,
        customClass: {
            confirmButton: 'btn btn-danger me-3',
            cancelButton: 'btn btn-secondary'
        },
        preConfirm: () => {
            $.ajax({
                type: "Get",
                url: url,
                success: function () {
                    window.location = returnPage;
                },
                error: function () {
                    alert("خطایی رخ داده است!");
                }
            });
        },
        backdrop: true,
        allowOutsideClick: () => !Swal.isLoading()
    });

}
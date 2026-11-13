const notificationBox = document.getElementById('notification');
if (notificationBox.dataset.success) {
    Swal.fire({
        icon: 'success',
        title: 'Başarılı',
        text: notificationBox.dataset.success,
        timer: 2000,
        showConfirmButton: false
    });
}
if (notificationBox.dataset.error) {
    Swal.fire({
        icon: 'error',
        title: 'İşlem yapılamadı',
        text: notificationBox.dataset.error,
        confirmButtonText: 'Tamam',
        confirmButtonColor: '#dc3545'
    });
}
// API Base URL
const API_BASE_URL = '/api';
let deleteType = null;
let deleteId = null;

// Show/Hide Loading
function showLoading() {
    document.getElementById('loading').classList.add('show');
}

function hideLoading() {
    document.getElementById('loading').classList.remove('show');
}

// Alert Management
function showAlert(message, type = 'success') {
    const alertContainer = document.getElementById('alertContainer');
    const alertId = 'alert-' + Date.now();
    const alertHTML = `
        <div class="alert alert-${type} alert-dismissible fade show" id="${alertId}" role="alert">
            <i class="bi bi-${type === 'success' ? 'check-circle' : 'exclamation-circle'}"></i> ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    `;
    alertContainer.innerHTML += alertHTML;
    
    setTimeout(() => {
        const alert = document.getElementById(alertId);
        if (alert) {
            alert.remove();
        }
    }, 5000);
}

// API Calls with Error Handling
async function apiCall(method, endpoint, data = null) {
    try {
        const options = {
            method: method,
            headers: {
                'Content-Type': 'application/json'
            }
        };

        if (data) {
            options.body = JSON.stringify(data);
        }

        const response = await fetch(`${API_BASE_URL}${endpoint}`, options);

        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || `HTTP Error: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        showAlert(`Lỗi: ${error.message}`, 'danger');
        throw error;
    }
}

// ==================== STUDENTS ====================

// Load Students
async function loadStudents() {
    showLoading();
    try {
        const students = await apiCall('GET', '/students');
        displayStudents(students);
    } catch (error) {
        console.error('Error loading students:', error);
    } finally {
        hideLoading();
    }
}

// Display Students in Table
function displayStudents(students) {
    const tbody = document.getElementById('studentTableBody');
    const emptyState = document.getElementById('emptyStudents');
    
    if (students.length === 0) {
        tbody.innerHTML = '';
        emptyState.style.display = 'block';
        return;
    }
    
    emptyState.style.display = 'none';
    tbody.innerHTML = students.map(student => `
        <tr>
            <td><span class="badge bg-info">${student.id}</span></td>
            <td><strong>${student.name}</strong></td>
            <td>${student.email}</td>
            <td>${student.age}</td>
            <td>${student.phoneNumber}</td>
            <td>${student.classId || '-'}</td>
            <td>
                <button class="btn btn-sm btn-info" onclick="viewStudent(${student.id})">
                    <i class="bi bi-eye"></i>
                </button>
                <button class="btn btn-sm btn-danger" onclick="openDeleteModal('student', ${student.id}, '${student.name}')">
                    <i class="bi bi-trash"></i>
                </button>
            </td>
        </tr>
    `).join('');
}

// Create Student
document.getElementById('studentForm')?.addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const student = {
        name: document.getElementById('studentName').value,
        email: document.getElementById('studentEmail').value,
        age: parseInt(document.getElementById('studentAge').value),
        phoneNumber: document.getElementById('studentPhoneNumber').value,
        classId: parseInt(document.getElementById('studentClassId').value) || 0
    };

    showLoading();
    try {
        await apiCall('POST', '/students', student);
        showAlert('Thêm sinh viên thành công!', 'success');
        document.getElementById('studentForm').reset();
        await loadStudents();
    } catch (error) {
        console.error('Error creating student:', error);
    } finally {
        hideLoading();
    }
});

// View Student Details
async function viewStudent(id) {
    try {
        const student = await apiCall('GET', `/students/${id}`);
        const details = `
            <strong>ID:</strong> ${student.id}<br>
            <strong>Tên:</strong> ${student.name}<br>
            <strong>Email:</strong> ${student.email}<br>
            <strong>Tuổi:</strong> ${student.age}<br>
            <strong>SĐT:</strong> ${student.phoneNumber}<br>
            <strong>Lớp:</strong> ${student.classId || 'Chưa xếp lớp'}
        `;
        
        const modal = new bootstrap.Modal(document.createElement('div'));
        alert(`Thông Tin Sinh Viên\n\nID: ${student.id}\nTên: ${student.name}\nEmail: ${student.email}\nTuổi: ${student.age}\nSĐT: ${student.phoneNumber}`);
    } catch (error) {
        console.error('Error viewing student:', error);
    }
}

// ==================== CLASSES ====================

// Load Classes
async function loadClasses() {
    showLoading();
    try {
        const classes = await apiCall('GET', '/classes');
        displayClasses(classes);
        populateClassSelect(classes);
    } catch (error) {
        console.error('Error loading classes:', error);
    } finally {
        hideLoading();
    }
}

// Display Classes in Table
function displayClasses(classes) {
    const tbody = document.getElementById('classTableBody');
    const emptyState = document.getElementById('emptyClasses');
    
    if (classes.length === 0) {
        tbody.innerHTML = '';
        emptyState.style.display = 'block';
        return;
    }
    
    emptyState.style.display = 'none';
    tbody.innerHTML = classes.map(classItem => `
        <tr>
            <td><span class="badge bg-info">${classItem.id}</span></td>
            <td><strong>${classItem.name}</strong></td>
            <td><span class="badge bg-success">${classItem.capacity}</span></td>
            <td>${classItem.description || '-'}</td>
            <td>
                <button class="btn btn-sm btn-info" onclick="viewClass(${classItem.id})">
                    <i class="bi bi-eye"></i>
                </button>
                <button class="btn btn-sm btn-danger" onclick="openDeleteModal('class', ${classItem.id}, '${classItem.name}')">
                    <i class="bi bi-trash"></i>
                </button>
            </td>
        </tr>
    `).join('');
}

// Populate Class Select
function populateClassSelect(classes) {
    const select = document.getElementById('studentClassId');
    const currentValue = select.value;
    
    select.innerHTML = '<option value="">Chọn lớp học</option>' + classes.map(classItem => `
        <option value="${classItem.id}">${classItem.name}</option>
    `).join('');
    
    if (currentValue) {
        select.value = currentValue;
    }
}

// Create Class
document.getElementById('classForm')?.addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const classData = {
        name: document.getElementById('className').value,
        capacity: parseInt(document.getElementById('classCapacity').value),
        description: document.getElementById('classDescription').value
    };

    showLoading();
    try {
        await apiCall('POST', '/classes', classData);
        showAlert('Thêm lớp học thành công!', 'success');
        document.getElementById('classForm').reset();
        await loadClasses();
    } catch (error) {
        console.error('Error creating class:', error);
    } finally {
        hideLoading();
    }
});

// View Class Details
async function viewClass(id) {
    try {
        const classItem = await apiCall('GET', `/classes/${id}`);
        alert(`Thông Tin Lớp Học\n\nID: ${classItem.id}\nTên: ${classItem.name}\nSức Chứa: ${classItem.capacity}\nMô Tả: ${classItem.description || 'Không có'}`);
    } catch (error) {
        console.error('Error viewing class:', error);
    }
}

// ==================== DELETE FUNCTIONALITY ====================

function openDeleteModal(type, id, name) {
    const modal = new bootstrap.Modal(document.getElementById('deleteModal'));
    const message = type === 'student' 
        ? `Bạn có chắc muốn xóa sinh viên <strong>${name}</strong>?`
        : `Bạn có chắc muốn xóa lớp học <strong>${name}</strong>?`;
    
    document.getElementById('deleteMessage').innerHTML = message;
    deleteType = type;
    deleteId = id;
    modal.show();
}

document.getElementById('confirmDelete')?.addEventListener('click', async () => {
    if (!deleteType || !deleteId) return;
    
    showLoading();
    try {
        const endpoint = deleteType === 'student' 
            ? `/students/${deleteId}` 
            : `/classes/${deleteId}`;
        
        await apiCall('DELETE', endpoint);
        
        const message = deleteType === 'student' 
            ? 'Xóa sinh viên thành công!' 
            : 'Xóa lớp học thành công!';
        
        showAlert(message, 'success');
        
        // Close modal and reload data
        bootstrap.Modal.getInstance(document.getElementById('deleteModal')).hide();
        
        if (deleteType === 'student') {
            await loadStudents();
        } else {
            await loadClasses();
        }
    } catch (error) {
        console.error('Error deleting:', error);
    } finally {
        hideLoading();
        deleteType = null;
        deleteId = null;
    }
});

// ==================== INITIALIZE ====================

document.addEventListener('DOMContentLoaded', async () => {
    // Load initial data
    await loadStudents();
    await loadClasses();
    
    // Set up tab switching
    document.getElementById('students-tab')?.addEventListener('shown.bs.tab', async () => {
        await loadStudents();
    });
    
    document.getElementById('classes-tab')?.addEventListener('shown.bs.tab', async () => {
        await loadClasses();
    });
});

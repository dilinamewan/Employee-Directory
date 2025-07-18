// Professional Employee Directory JavaScript
document.addEventListener('DOMContentLoaded', function () {
    // Initialize animations
    initializeAnimations();

    // Initialize search functionality
    initializeSearch();

    // Initialize table interactions
    initializeTableInteractions();

    // Initialize tooltips
    initializeTooltips();

    // Initialize loading states
    initializeLoadingStates();

    // Initialize form validation
    initializeFormValidation();

    // Initialize smooth scrolling
    initializeSmoothScrolling();
});

// Animation initialization
function initializeAnimations() {
    // Subtle fade in animations for cards
    const cards = document.querySelectorAll('.card, .feature-card, .stats-card');
    cards.forEach((card, index) => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(10px)';

        setTimeout(() => {
            card.style.transition = 'all 0.3s ease-out';
            card.style.opacity = '1';
            card.style.transform = 'translateY(0)';
        }, index * 50);
    });
}

// Search functionality
function initializeSearch() {
    const searchForm = document.querySelector('form[asp-action="Index"]');
    const searchInputs = document.querySelectorAll('input[type="text"]');

    if (searchForm) {
        // Add real-time search delay
        let searchTimeout;

        searchInputs.forEach(input => {
            input.addEventListener('input', function () {
                clearTimeout(searchTimeout);

                // Debounce search
                searchTimeout = setTimeout(() => {
                    // Auto-submit form after 1 second of no typing
                    if (this.value.length > 2 || this.value.length === 0) {
                        // Uncomment to enable auto-search
                        // searchForm.submit();
                    }
                }, 1000);
            });
        });

        // Enhanced form submission
        searchForm.addEventListener('submit', function (e) {
            const submitButton = this.querySelector('button[type="submit"]');
            if (submitButton) {
                showButtonLoading(submitButton, true);
            }
        });
    }
}

// Table interactions
function initializeTableInteractions() {
    const tableRows = document.querySelectorAll('.employee-row, tbody tr');

    tableRows.forEach(row => {
        // Add subtle hover effects
        row.addEventListener('mouseenter', function () {
            this.style.backgroundColor = 'rgba(44, 62, 80, 0.05)';
            this.style.transition = 'background-color 0.2s ease';
        });

        row.addEventListener('mouseleave', function () {
            this.style.backgroundColor = '';
        });
    });

    // Action button hover effects
    const actionButtons = document.querySelectorAll('.btn-group .btn, .action-buttons .btn');
    actionButtons.forEach(btn => {
        btn.addEventListener('mouseenter', function () {
            this.style.transform = 'scale(1.05)';
            this.style.transition = 'transform 0.2s ease';
        });

        btn.addEventListener('mouseleave', function () {
            this.style.transform = 'scale(1)';
        });
    });
}

// Tooltip initialization
function initializeTooltips() {
    // Enable Bootstrap tooltips if available
    if (typeof bootstrap !== 'undefined' && bootstrap.Tooltip) {
        const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"], [title]'));
        tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    }
}

// Loading states
function initializeLoadingStates() {
    // Reset any buttons that might be stuck in loading state
    const loadingButtons = document.querySelectorAll('button[type="submit"]');
    loadingButtons.forEach(button => {
        if (button.innerHTML.includes('Loading...')) {
            showButtonLoading(button, false);
        }
    });

    // Add loading spinner to buttons
    const submitButtons = document.querySelectorAll('button[type="submit"]');

    submitButtons.forEach(button => {
        button.addEventListener('click', function (e) {
            // Don't show loading if there are validation errors
            const form = this.form;
            if (form) {
                // Check for validation errors in the form
                const hasErrors = form.querySelector('.field-validation-error, .text-danger') !== null;
                const hasValidationSummary = form.querySelector('.validation-summary-errors') !== null;
                
                if (!hasErrors && !hasValidationSummary) {
                    // Small delay to allow form submission to process
                    setTimeout(() => {
                        if (form.checkValidity()) {
                            showButtonLoading(this, true);
                        }
                    }, 100);
                }
            }
        });
    });

    // Reset loading state when page loads (in case of validation errors)
    window.addEventListener('load', function() {
        const buttons = document.querySelectorAll('button[type="submit"]');
        buttons.forEach(button => {
            if (button.innerHTML.includes('Loading...') || button.disabled) {
                showButtonLoading(button, false);
            }
        });
    });
}

// Show button loading state
function showButtonLoading(button, show) {
    if (show) {
        const originalText = button.innerHTML;
        button.dataset.originalText = originalText;
        button.disabled = true;
        button.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Loading...';
    } else {
        button.disabled = false;
        button.innerHTML = button.dataset.originalText || button.innerHTML;
    }
}

// Enhanced form validation
function initializeFormValidation() {
    const forms = document.querySelectorAll('form');

    forms.forEach(form => {
        // Only add visual feedback, don't prevent submission
        // Let ASP.NET Core handle the actual validation
        const requiredFields = form.querySelectorAll('[required]');
        
        requiredFields.forEach(field => {
            field.addEventListener('blur', function() {
                if (!this.value.trim()) {
                    showFieldError(this, 'This field is required');
                } else {
                    clearFieldError(this);
                }
            });

            field.addEventListener('input', function() {
                if (this.value.trim()) {
                    clearFieldError(this);
                }
            });
        });

        // Phone number specific validation
        const phoneFields = form.querySelectorAll('input[name="Phone"]');
        phoneFields.forEach(field => {
            field.addEventListener('input', function(e) {
                // Remove non-numeric characters
                this.value = this.value.replace(/\D/g, '');
                
                // Limit to 10 digits
                if (this.value.length > 10) {
                    this.value = this.value.slice(0, 10);
                }
            });

            field.addEventListener('blur', function() {
                if (this.value.length > 0 && this.value.length !== 10) {
                    showFieldError(this, 'Phone number must be exactly 10 digits');
                } else if (this.value.length === 10) {
                    clearFieldError(this);
                }
            });
        });
    });
}

// Show field error
function showFieldError(field, message) {
    field.classList.add('is-invalid');

    let errorDiv = field.parentNode.querySelector('.invalid-feedback');
    if (!errorDiv) {
        errorDiv = document.createElement('div');
        errorDiv.className = 'invalid-feedback';
        field.parentNode.appendChild(errorDiv);
    }

    errorDiv.textContent = message;
}

// Clear field error
function clearFieldError(field) {
    field.classList.remove('is-invalid');
    const errorDiv = field.parentNode.querySelector('.invalid-feedback');
    if (errorDiv) {
        errorDiv.remove();
    }
}

// Show notification
function showNotification(message, type = 'info') {
    const alertClass = type === 'error' ? 'alert-danger' : `alert-${type}`;
    const notification = document.createElement('div');
    notification.className = `alert ${alertClass} alert-dismissible fade show`;
    notification.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;

    // Add notification styles
    notification.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        z-index: 1050;
        max-width: 400px;
        animation: slideInRight 0.3s ease-out;
    `;

    document.body.appendChild(notification);

    // Auto-remove after 5 seconds
    setTimeout(() => {
        if (notification.parentNode) {
            notification.remove();
        }
    }, 5000);
}

// Smooth scrolling for anchor links
function initializeSmoothScrolling() {
    const anchorLinks = document.querySelectorAll('a[href^="#"]');

    anchorLinks.forEach(link => {
        link.addEventListener('click', function (e) {
            e.preventDefault();

            const targetId = this.getAttribute('href').substring(1);
            const targetElement = document.getElementById(targetId);

            if (targetElement) {
                targetElement.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });
}

// Add keyboard navigation
document.addEventListener('keydown', function (e) {
    // Quick search with Ctrl+F
    if (e.ctrlKey && e.key === 'f') {
        e.preventDefault();
        const searchInput = document.querySelector('input[type="text"]');
        if (searchInput) {
            searchInput.focus();
        }
    }

    // Quick add with Ctrl+N
    if (e.ctrlKey && e.key === 'n') {
        e.preventDefault();
        const addButton = document.querySelector('a[href*="/Create"]');
        if (addButton) {
            addButton.click();
        }
    }
});


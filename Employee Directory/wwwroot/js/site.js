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

    // Initialize smooth scrolling
    initializeSmoothScrolling();

    // Simplified form handling - no interference with ASP.NET Core validation
    initializeSimpleFormHandling();
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

// Simplified form handling - no interference with validation
function initializeSimpleFormHandling() {
    // Just add basic visual feedback, don't prevent submission
    const forms = document.querySelectorAll('form');
    
    forms.forEach(form => {
        const submitButton = form.querySelector('button[type="submit"]');
        if (submitButton) {
            form.addEventListener('submit', function() {
                // Simple loading state with timeout
                if (submitButton) {
                    const originalText = submitButton.innerHTML;
                    submitButton.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Loading...';
                    submitButton.disabled = true;
                    
                    // Reset after 15 seconds as fallback
                    setTimeout(() => {
                        submitButton.innerHTML = originalText;
                        submitButton.disabled = false;
                    }, 15000);
                }
            });
        }
    });
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


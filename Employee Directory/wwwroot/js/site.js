/**
 * Professional Employee Directory JavaScript
 * 
 * This file contains all client-side functionality for the Employee Directory application.
 * It handles animations, user interactions, form enhancements, and UI improvements
 * while being careful not to interfere with ASP.NET Core's server-side validation.
 * 
 * @author Employee Directory Team
 * @version 1.0
 */

// Wait for the DOM to be fully loaded before initializing any functionality
document.addEventListener('DOMContentLoaded', function () {
    // Initialize subtle fade-in animations for better user experience
    initializeAnimations();

    // Initialize enhanced search functionality with debouncing
    initializeSearch();

    // Initialize interactive table row and button hover effects
    initializeTableInteractions();

    // Initialize Bootstrap tooltips for better accessibility
    initializeTooltips();

    // Initialize smooth scrolling for anchor links navigation
    initializeSmoothScrolling();

    // Initialize form loading states without interfering with server-side validation
    initializeSimpleFormHandling();
});

/**
 * Initializes subtle fade-in animations for UI cards and elements
 * 
 * This function creates a staggered animation effect where cards appear with a subtle
 * fade-in and slide-up motion. Each card has a slightly delayed animation start time
 * to create a pleasant cascading effect that improves perceived performance.
 * 
 * Elements targeted: .card, .feature-card, .stats-card
 * Animation: 300ms ease-out transition with 10px vertical translation
 * Stagger delay: 50ms between each element
 */
function initializeAnimations() {
    // Select all card elements that should have fade-in animation
    const cards = document.querySelectorAll('.card, .feature-card, .stats-card');
    
    cards.forEach((card, index) => {
        // Set initial state: invisible and slightly translated down
        card.style.opacity = '0';
        card.style.transform = 'translateY(10px)';

        // Apply animation with staggered delay for each card
        setTimeout(() => {
            // Set transition properties for smooth animation
            card.style.transition = 'all 0.3s ease-out';
            // Animate to final state: fully visible and in original position
            card.style.opacity = '1';
            card.style.transform = 'translateY(0)';
        }, index * 50); // 50ms delay between each card animation
    });
}

/**
 * Initializes enhanced search functionality with debouncing and auto-submission
 * 
 * This function adds real-time search capabilities to search forms while implementing
 * debouncing to prevent excessive server requests. It also provides visual feedback
 * during search operations by showing loading states on submit buttons.
 * 
 * Features:
 * - 1-second debounce delay to prevent rapid API calls
 * - Auto-submission when search term is longer than 2 characters or empty
 * - Loading state indication during form submission
 * - Targets forms with asp-action="Index" attribute
 * 
 * @note Auto-search is currently commented out to prevent interference with server-side pagination
 */
function initializeSearch() {
    // Find the main search form (typically the employee index search)
    const searchForm = document.querySelector('form[asp-action="Index"]');
    const searchInputs = document.querySelectorAll('input[type="text"]');

    if (searchForm) {
        // Variable to store timeout reference for debouncing
        let searchTimeout;

        // Add input event listeners to all text inputs for real-time search
        searchInputs.forEach(input => {
            input.addEventListener('input', function () {
                // Clear any existing timeout to implement debouncing
                clearTimeout(searchTimeout);

                // Set new timeout for delayed search execution
                searchTimeout = setTimeout(() => {
                    // Only trigger search for meaningful input (>2 chars) or when cleared
                    if (this.value.length > 2 || this.value.length === 0) {
                        // Auto-submit functionality is commented out to avoid conflicts
                        // with server-side pagination and filtering logic
                        // searchForm.submit();
                    }
                }, 1000); // 1-second debounce delay
            });
        });

        // Enhanced form submission with loading state feedback
        searchForm.addEventListener('submit', function (e) {
            const submitButton = this.querySelector('button[type="submit"]');
            if (submitButton) {
                // Show loading state using the same approach as initializeSimpleFormHandling
                const originalText = submitButton.innerHTML;
                submitButton.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Searching...';
                submitButton.disabled = true;
                
                // Reset after 10 seconds for search operations (shorter than general forms)
                setTimeout(() => {
                    submitButton.innerHTML = originalText;
                    submitButton.disabled = false;
                }, 10000);
            }
        });
    }
}

/**
 * Initializes interactive effects for table rows and action buttons
 * 
 * This function enhances user experience by adding subtle hover effects to
 * table rows and action buttons. These effects provide visual feedback when
 * users interact with clickable elements in the employee directory.
 * 
 * Table Row Effects:
 * - Subtle background color change on hover (light blue tint)
 * - Smooth 200ms transition for professional feel
 * - Targets: .employee-row, tbody tr elements
 * 
 * Button Effects:
 * - Scale transform (1.05x) on hover for emphasis
 * - Smooth 200ms transition
 * - Targets: .btn-group .btn, .action-buttons .btn elements
 */
function initializeTableInteractions() {
    // Select all table rows that should have hover effects
    const tableRows = document.querySelectorAll('.employee-row, tbody tr');

    tableRows.forEach(row => {
        // Mouse enter event - show hover state
        row.addEventListener('mouseenter', function () {
            // Apply subtle blue background tint with transparency
            this.style.backgroundColor = 'rgba(44, 62, 80, 0.05)';
            // Smooth transition for professional appearance
            this.style.transition = 'background-color 0.2s ease';
        });

        // Mouse leave event - remove hover state
        row.addEventListener('mouseleave', function () {
            // Reset background to default (empty string uses CSS default)
            this.style.backgroundColor = '';
        });
    });

    // Select all action buttons for hover effects
    const actionButtons = document.querySelectorAll('.btn-group .btn, .action-buttons .btn');
    actionButtons.forEach(btn => {
        // Mouse enter event - show button emphasis
        btn.addEventListener('mouseenter', function () {
            // Slightly scale up the button (5% increase)
            this.style.transform = 'scale(1.05)';
            // Smooth transition for the scale effect
            this.style.transition = 'transform 0.2s ease';
        });

        // Mouse leave event - return to normal size
        btn.addEventListener('mouseleave', function () {
            // Reset scale to normal size
            this.style.transform = 'scale(1)';
        });
    });
}

/**
 * Initializes Bootstrap tooltips for enhanced accessibility and user guidance
 * 
 * This function automatically enables Bootstrap tooltip functionality for all
 * elements that have either the 'data-bs-toggle="tooltip"' attribute or a
 * 'title' attribute. This improves accessibility and provides helpful context
 * to users about various UI elements.
 * 
 * Requirements:
 * - Bootstrap JavaScript library must be loaded
 * - Elements should have data-bs-toggle="tooltip" or title attribute
 * - Tooltip content should be specified in title or data-bs-title attributes
 * 
 * @note Function includes safety check for Bootstrap availability
 */
function initializeTooltips() {
    // Check if Bootstrap library is available and has Tooltip component
    if (typeof bootstrap !== 'undefined' && bootstrap.Tooltip) {
        // Find all elements that should have tooltips
        // This includes elements with data-bs-toggle="tooltip" or any title attribute
        const tooltipTriggerList = [].slice.call(
            document.querySelectorAll('[data-bs-toggle="tooltip"], [title]')
        );
        
        // Initialize Bootstrap tooltip for each element
        tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    } else {
        // Log warning if Bootstrap is not available (development debugging)
        console.warn('Bootstrap library not found. Tooltips will not be initialized.');
    }
}

/**
 * Initializes simple form handling with loading states and user feedback
 * 
 * This function enhances form submission by providing visual feedback to users
 * without interfering with ASP.NET Core's built-in validation system. When a
 * form is submitted, the submit button shows a loading spinner and becomes
 * disabled to prevent duplicate submissions.
 * 
 * Features:
 * - Shows loading spinner on form submission
 * - Disables submit button to prevent double-submission
 * - 15-second timeout fallback to reset button state
 * - Non-intrusive - doesn't prevent form submission or validation
 * 
 * @note This approach respects server-side validation and error handling
 */
function initializeSimpleFormHandling() {
    // Find all forms on the page
    const forms = document.querySelectorAll('form');
    
    forms.forEach(form => {
        // Look for submit button in each form
        const submitButton = form.querySelector('button[type="submit"]');
        
        if (submitButton) {
            // Add submit event listener to the form
            form.addEventListener('submit', function() {
                // Store the original button text to restore later
                const originalText = submitButton.innerHTML;
                
                // Update button to show loading state
                submitButton.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Loading...';
                submitButton.disabled = true;
                
                // Fallback timeout to reset button state after 15 seconds
                // This handles cases where form submission fails or takes very long
                setTimeout(() => {
                    submitButton.innerHTML = originalText;
                    submitButton.disabled = false;
                }, 15000);
            });
        }
    });
}

/**
 * Initializes smooth scrolling behavior for internal anchor links
 * 
 * This function enhances navigation by providing smooth scrolling animation
 * when users click on internal anchor links (href="#..."). Instead of the
 * default instant jump behavior, users experience a smooth scroll animation
 * to the target section.
 * 
 * Features:
 * - Smooth scroll animation for better UX
 * - Targets all links with href starting with "#"
 * - Scrolls to 'start' of target element
 * - Graceful handling of missing target elements
 * 
 * @note Prevents default browser jump behavior
 */
function initializeSmoothScrolling() {
    // Find all anchor links that point to internal sections (starting with #)
    const anchorLinks = document.querySelectorAll('a[href^="#"]');

    anchorLinks.forEach(link => {
        link.addEventListener('click', function (e) {
            // Prevent the default instant jump behavior
            e.preventDefault();

            // Extract the target element ID from the href (remove the # symbol)
            const targetId = this.getAttribute('href').substring(1);
            const targetElement = document.getElementById(targetId);

            // Only scroll if the target element exists on the page
            if (targetElement) {
                // Use modern smooth scrolling API for better performance
                targetElement.scrollIntoView({
                    behavior: 'smooth',    // Smooth animation instead of instant
                    block: 'start'        // Align to the start of the target element
                });
            } else {
                // Log warning for missing targets (development debugging)
                console.warn(`Smooth scroll target not found: ${targetId}`);
            }
        });
    });
}

/**
 * Global keyboard navigation and shortcuts for improved accessibility
 * 
 * This section implements keyboard shortcuts that allow power users to
 * navigate the application more efficiently. These shortcuts follow common
 * web application conventions and improve accessibility.
 * 
 * Available shortcuts:
 * - Ctrl+F: Focus on search input (overrides browser's find function)
 * - Ctrl+N: Navigate to "Add New Employee" page
 */
document.addEventListener('keydown', function (e) {
    // Quick search shortcut: Ctrl+F
    if (e.ctrlKey && e.key === 'f') {
        // Prevent browser's default find dialog
        e.preventDefault();
        
        // Find the first text input (usually the search field)
        const searchInput = document.querySelector('input[type="text"]');
        if (searchInput) {
            // Focus and select the search input for immediate typing
            searchInput.focus();
            searchInput.select(); // Select existing text for easy replacement
        }
    }

    // Quick add employee shortcut: Ctrl+N
    if (e.ctrlKey && e.key === 'n') {
        // Prevent browser's default new window/tab behavior
        e.preventDefault();
        
        // Find the "Create" or "Add Employee" link
        const addButton = document.querySelector('a[href*="/Create"]');
        if (addButton) {
            // Navigate to the create employee page
            addButton.click();
        }
    }
});

/**
 * Utility function to show loading state on buttons (referenced but not implemented)
 * 
 * @note This function is referenced in the search initialization but not yet implemented.
 * It should handle showing/hiding loading states on buttons during form submissions.
 * 
 * @param {HTMLElement} button - The button element to modify
 * @param {boolean} loading - Whether to show or hide loading state
 * 
 * @todo Implement this function if additional loading states are needed beyond
 * the basic functionality already provided in initializeSimpleFormHandling()
 */
// function showButtonLoading(button, loading) {
//     // Implementation would go here if needed
// }


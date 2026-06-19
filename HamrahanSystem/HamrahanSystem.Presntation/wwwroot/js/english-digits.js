(function () {
    "use strict";

    var localizedDigitPattern = /[۰-۹٠-٩]/g;
    var localizedDigitCheckPattern = /[۰-۹٠-٩]/;
    var ignoredTags = {
        SCRIPT: true,
        STYLE: true,
        NOSCRIPT: true,
        TEMPLATE: true
    };
    var ignoredInputTypes = {
        button: true,
        checkbox: true,
        file: true,
        hidden: true,
        image: true,
        password: true,
        radio: true,
        reset: true,
        submit: true
    };
    var pending = false;
    var queuedRoots = [];
    var observer = null;

    function toEnglishDigits(value) {
        if (value == null) {
            return value;
        }

        return String(value).replace(localizedDigitPattern, function (digit) {
            var code = digit.charCodeAt(0);
            if (code >= 1776 && code <= 1785) {
                return String(code - 1776);
            }
            return String(code - 1632);
        });
    }

    function hasLocalizedDigits(value) {
        return value != null && localizedDigitCheckPattern.test(String(value));
    }

    function isIgnoredElement(element) {
        while (element && element.nodeType === 1) {
            if (ignoredTags[element.tagName] || element.hasAttribute("data-keep-persian-digits")) {
                return true;
            }
            element = element.parentElement;
        }
        return false;
    }

    function normalizeTextNode(node) {
        if (!node || !node.nodeValue || !hasLocalizedDigits(node.nodeValue) || isIgnoredElement(node.parentElement)) {
            return;
        }
        node.nodeValue = toEnglishDigits(node.nodeValue);
    }

    function normalizeAttribute(element, name) {
        var value = element.getAttribute(name);
        if (hasLocalizedDigits(value)) {
            element.setAttribute(name, toEnglishDigits(value));
        }
    }

    function normalizeElementAttributes(element) {
        if (!element || element.nodeType !== 1 || isIgnoredElement(element)) {
            return;
        }

        normalizeAttribute(element, "placeholder");
        normalizeAttribute(element, "title");
        normalizeAttribute(element, "aria-label");
        normalizeAttribute(element, "aria-valuetext");
    }

    function isTextInput(element) {
        if (!element || element.tagName !== "INPUT") {
            return false;
        }

        return !ignoredInputTypes[String(element.type || "text").toLowerCase()];
    }

    function normalizeControlValue(element) {
        if (!element || element.nodeType !== 1 || isIgnoredElement(element)) {
            return;
        }

        if (isTextInput(element) || element.tagName === "TEXTAREA") {
            var value = element.value;
            if (!hasLocalizedDigits(value)) {
                return;
            }

            var start = null;
            var end = null;
            try {
                start = element.selectionStart;
                end = element.selectionEnd;
            } catch (error) {
                start = null;
                end = null;
            }
            element.value = toEnglishDigits(value);

            if (typeof start === "number" && typeof end === "number" && document.activeElement === element) {
                try {
                    element.setSelectionRange(start, end);
                } catch (error) {
                    // Some input types do not support text selection.
                }
            }
            return;
        }

        if (element.tagName === "OPTION") {
            if (hasLocalizedDigits(element.text)) {
                element.text = toEnglishDigits(element.text);
            }
            if (element.parentElement && element.parentElement.tagName === "DATALIST" && hasLocalizedDigits(element.value)) {
                element.value = toEnglishDigits(element.value);
            }
        }
    }

    function normalizeSubtree(root) {
        if (!root) {
            return;
        }

        if (root.nodeType === 3) {
            normalizeTextNode(root);
            return;
        }

        if (root.nodeType !== 1 && root.nodeType !== 9 && root.nodeType !== 11) {
            return;
        }

        if (root.nodeType === 1) {
            if (isIgnoredElement(root)) {
                return;
            }
            normalizeElementAttributes(root);
            normalizeControlValue(root);
        }

        if (root.querySelectorAll) {
            root.querySelectorAll("input, textarea, option, [placeholder], [title], [aria-label], [aria-valuetext]").forEach(function (element) {
                normalizeElementAttributes(element);
                normalizeControlValue(element);
            });
        }

        var walker = document.createTreeWalker(root, NodeFilter.SHOW_TEXT, {
            acceptNode: function (node) {
                return isIgnoredElement(node.parentElement) ? NodeFilter.FILTER_REJECT : NodeFilter.FILTER_ACCEPT;
            }
        });

        while (walker.nextNode()) {
            normalizeTextNode(walker.currentNode);
        }
    }

    function scheduleNormalize(root) {
        if (root) {
            queuedRoots.push(root);
        }

        if (pending) {
            return;
        }

        pending = true;
        window.requestAnimationFrame(function () {
            var roots = queuedRoots.length ? queuedRoots.slice(0) : [document.body];
            queuedRoots = [];
            pending = false;

            roots.forEach(function (root) {
                normalizeSubtree(root);
            });
        });
    }

    function handleControlEvent(event) {
        var target = event.target;
        if (!target || target.nodeType !== 1) {
            return;
        }

        if (event.type === "paste") {
            window.setTimeout(function () {
                normalizeControlValue(target);
            }, 0);
            return;
        }

        normalizeControlValue(target);
    }

    function startObserver() {
        if (!window.MutationObserver || !document.body) {
            return;
        }

        observer = new MutationObserver(function (mutations) {
            mutations.forEach(function (mutation) {
                if (mutation.type === "characterData") {
                    scheduleNormalize(mutation.target);
                    return;
                }

                if (mutation.type === "attributes") {
                    scheduleNormalize(mutation.target);
                    return;
                }

                mutation.addedNodes.forEach(function (node) {
                    scheduleNormalize(node);
                });
            });
        });

        observer.observe(document.body, {
            childList: true,
            subtree: true,
            characterData: true,
            attributes: true,
            attributeFilter: ["placeholder", "title", "aria-label", "aria-valuetext"]
        });
    }

    function bindEvents() {
        ["input", "change", "blur", "keyup", "paste", "compositionend"].forEach(function (eventName) {
            document.addEventListener(eventName, handleControlEvent, true);
        });

        if (window.jQuery) {
            window.jQuery(document).ajaxComplete(function () {
                scheduleNormalize(document.body);
            });
            window.jQuery(document).on("select2:open select2:select select2:clear", function () {
                scheduleNormalize(document.body);
            });
        }
    }

    function init() {
        normalizeSubtree(document.body);
        startObserver();
        bindEvents();
        [100, 500, 1500].forEach(function (delay) {
            window.setTimeout(function () {
                scheduleNormalize(document.body);
            }, delay);
        });
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", init);
    } else {
        init();
    }

    window.AppEnglishDigits = {
        toEnglishDigits: toEnglishDigits,
        normalize: normalizeSubtree
    };
})();

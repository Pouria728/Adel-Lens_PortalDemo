'use strict;'
class Searcher {
    static ToolbarDotNet;
    static NotFoundText = "Not found";

    static setSearcherProps(toolbarDotNet, notFoundText) {
        Searcher.ToolbarDotNet = toolbarDotNet;
        Searcher.NotFoundText = notFoundText;
    }

    static resetIndex() {
        sessionStorage.setItem('fastreport-search-text', '');
    }

    static toggleSearchForm() {
        var form = document.getElementById('fr-WebReport-toolbar-search-form');
        var searchText = sessionStorage.getItem('fastreport-search-text');
        var matchCase = sessionStorage.getItem('fastreport-search-match-case') === 'true';
        var wholeWord = sessionStorage.getItem('fastreport-search-whole-word') === 'true';
        form.classList.toggle('open');

        if (!form.classList.contains('open')) {
            this.findNext(
                0,
                searchText,
                matchCase,
                wholeWord,
                true
            );
        }
        else {
            this.findNext(
                sessionStorage.getItem('fastreport-search-index'),
                searchText,
                matchCase,
                wholeWord,
                false
            );
        }
    }


    static getSearchRanges(text, matchCase, wholeWord, container) {
        var sel = window.getSelection();
        var ranges = []
        // find all occurrences in a page
        while (window.find(text, matchCase, false, false, wholeWord, false, false)) {
            // filter out search results outside of a specific element
            if (container.contains(sel.anchorNode)) {
                ranges.push(sel.getRangeAt(sel.rangeCount - 1));
            }
        }
        return ranges
    };

    static findNext(index, text, matchCase, wholeWord, removeHighlight) {
        var container = Searcher._findContainer();
        // selection object
        var sel = window.getSelection();

        sel.collapse(container, 0)
        var ranges = Searcher.getSearchRanges(text, matchCase, wholeWord, container);
        sel.collapse(container, 0);

        if (ranges.length == 0) {
            return false;
        }
        else if ((index < ranges.length && index >= 0) || removeHighlight) {
            if (!removeHighlight) {
                ranges.sort((a, b) => {
                    return a.startContainer.parentElement.getBoundingClientRect().top - b.startContainer.parentElement.getBoundingClientRect().top
                });
                ranges = [ranges[index]];
            }
            else {
                ranges = ranges.filter((r) => r.startContainer.parentElement.classList.contains('search-highlight'));
            }

            for (var i = 0; i < ranges.length; i++) {
                var range = ranges[i]
                if (range.startContainer == range.endContainer) {
                    // Range includes just one node
                    if (removeHighlight)
                        Searcher.clearHighlight(range)
                    else
                        Searcher.highlight(range)
                    return true
                } else {
                    // More complex case: range includes multiple nodes
                    // Get all the text nodes in the range
                    var textNodes = Searcher.getTextNodesInRange(
                        range.commonAncestorContainer,
                        range.startContainer,
                        range.endContainer)

                    var startOffset = range.startOffset
                    var endOffset = range.endOffset
                    for (var j = 0; j < textNodes.length; j++) {
                        var node = textNodes[j]
                        range.setStart(node, j == 0 ? startOffset : 0)
                        range.setEnd(node, j == textNodes.length - 1 ?
                            endOffset : node.nodeValue.length)
                        if (removeHighlight)
                            Searcher.clearHighlight(range);
                        else
                            Searcher.highlight(range);
                    }
                }
                return true;
            }

        }
        return false;
    };

    static search(backward) {
        var searchText = document.getElementById('fr-search-text').value
        var lastSearchText = sessionStorage.getItem('fastreport-search-text')
        var index = sessionStorage.getItem('fastreport-search-index')
        var matchCase = document.getElementById('fr-match-case').checked
        var wholeWord = document.getElementById('fr-whole-word').checked
        document.getElementById('fr-WebRepot-text-info').innerText = '';
        if (!index)
            index = -1

        if (lastSearchText) {
            Searcher.findNext(index, lastSearchText, sessionStorage.getItem('fastreport-search-match-case') === 'true', sessionStorage.getItem('fastreport-search-whole-word') === 'true', true)
        }

        if (backward)
            index--
        else
            index++

        if (lastSearchText != searchText)
            index = 0;

        if (!Searcher.findNext(index, searchText, matchCase, wholeWord, false)) {
            var that = Searcher;
            var container = Searcher._findContainer();

            // search on next pages
            Searcher.ToolbarDotNet.invokeMethodAsync('SearchText', searchText, backward, matchCase, wholeWord).then((value) => {
                if (value) {
                    index = backward ? that.getSearchRanges(searchText, matchCase, wholeWord, container).length - 1 : 0;
                    sessionStorage.setItem('fastreport-search-index', index);
                    if (!that.findNext(index, searchText, matchCase, wholeWord, false))
                        document.getElementById('fr-WebRepot-text-info').innerText = Searcher.NotFoundText;
                }
                else {
                    index = backward ? 0 : index - 1;
                    that.findNext(index, searchText, matchCase, wholeWord, false);
                    sessionStorage.setItem('fastreport-search-index', index);
                    document.getElementById('fr-WebRepot-text-info').innerText = Searcher.NotFoundText;
                }
            });
        }
        sessionStorage.setItem('fastreport-search-index', index);
        sessionStorage.setItem('fastreport-search-text', searchText);
        sessionStorage.setItem('fastreport-search-match-case', matchCase);
        sessionStorage.setItem('fastreport-search-whole-word', wholeWord);
    };

    static _findContainer() {
        {
            return document.getElementsByClassName('fr-webreport-body')[0];
        }
    };

    static highlight(range) {
        var newNode = document.createElement('span');
        newNode.className = 'search-highlight';
        range.surroundContents(newNode);
        const rect = newNode.getBoundingClientRect();
        const vWidth = window.innerWidth || doc.documentElement.clientWidth;
        const vHeight = window.innerHeight || doc.documentElement.clientHeight;

        // Check if the element is out of bounds
        if (rect.right < 0 || rect.bottom < 0 || rect.left > vWidth || rect.top > vHeight) {
            newNode.scrollIntoView();
        }
    };

    static clearHighlight (range) {
        var selection = document.getSelection()
        selection.removeAllRanges()
        selection.addRange(range)
        const selParent = selection.anchorNode?.parentElement;
        const selectedElem = selParent?.nodeType == 1 && selParent?.children.length < 2 && selParent;
        if (selectedElem.tagName === 'SPAN' && selectedElem.classList.contains('search-highlight')) {
            selectedElem.previousSibling.nodeValue += selectedElem.innerText;
            selectedElem.previousSibling.nodeValue += selectedElem.nextSibling.nodeValue;
            selectedElem.parentNode.removeChild(selectedElem.nextSibling);
            selectedElem.parentNode.removeChild(selectedElem);
        }
    };

    static getTextNodesInRange(rootNode, firstNode, lastNode) {
        var nodes = []
        var startNode = null, endNode = lastNode
        var walker = document.createTreeWalker(
            rootNode,
            // search for text nodes
            NodeFilter.SHOW_TEXT,
            // Logic to determine whether to accept, reject or skip node.
            // In Searcher case, only accept nodes that are between
            // <code>firstNode</code> and <code>lastNode</code>
            {
                acceptNode: function (node) {
                    if (!startNode) {
                        if (firstNode == node) {
                            startNode = node
                            return NodeFilter.FILTER_ACCEPT
                        }
                        return NodeFilter.FILTER_REJECT
                    }

                    if (endNode) {
                        if (lastNode == node) {
                            endNode = null
                        }
                        return NodeFilter.FILTER_ACCEPT
                    }

                    return NodeFilter.FILTER_REJECT
                }
            },
            false
        )

        while (walker.nextNode()) {
            nodes.push(walker.currentNode)
        }
        return nodes
    }
}

function addHandlers() {
    const searchPrev = document.getElementById("fr-search-prev");
    searchPrev.addEventListener("click", function () { Searcher.search(true) });

    const searchNext = document.getElementById("fr-search-next");
    searchNext.addEventListener("click", function () { Searcher.search(false) });
}
export { Searcher, addHandlers };

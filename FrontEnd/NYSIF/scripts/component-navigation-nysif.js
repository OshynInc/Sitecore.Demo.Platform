(function () {
  'use strict';

  function initNysifNavigation() {
    var root = document.querySelector('.navigation.nysif-mega-nav, .navigation.nysif-header-dark');
    if (!root) {
      return;
    }

    /* Desktop mega menu: click toggle */
    root.querySelectorAll('.nav-tab').forEach(function (tab) {
      tab.addEventListener('click', function (e) {
        e.stopPropagation();
        var menu = tab.closest('.nav-item').querySelector('.mega-menu');
        if (!menu) {
          return;
        }
        var isOpen = menu.classList.contains('open');
        document.querySelectorAll('.mega-menu.open').forEach(function (m) {
          m.classList.remove('open');
        });
        document.querySelectorAll('.nav-tab').forEach(function (t) {
          t.setAttribute('aria-expanded', 'false');
        });
        if (!isOpen) {
          menu.classList.add('open');
          tab.setAttribute('aria-expanded', 'true');
        }
      });
    });

    root.querySelectorAll('.mega-menu').forEach(function (menu) {
      menu.addEventListener('click', function (e) {
        e.stopPropagation();
      });
    });

    document.addEventListener('click', function () {
      document.querySelectorAll('.mega-menu.open').forEach(function (m) {
        m.classList.remove('open');
      });
      document.querySelectorAll('.nav-tab').forEach(function (t) {
        t.setAttribute('aria-expanded', 'false');
      });
    });

    /* Employer mega menu: swap second column */
    root.querySelectorAll('.mega-link-expand').forEach(function (btn) {
      function activate() {
        var menu = btn.closest('.mega-menu');
        menu.querySelectorAll('.mega-link-expand').forEach(function (b) {
          b.classList.remove('active-sub');
        });
        btn.classList.add('active-sub');
        menu.querySelectorAll('.mega-col-2').forEach(function (c) {
          c.classList.remove('active');
        });
        var target = menu.querySelector('.mega-col-2[data-sub-panel="' + btn.dataset.sub + '"]');
        if (target) {
          target.classList.add('active');
        }
      }
      btn.addEventListener('mouseenter', activate);
      btn.addEventListener('click', function (e) {
        e.preventDefault();
        activate();
      });
    });

    root.querySelectorAll('.mega-col-1 > .mega-link:not(.mega-link-expand)').forEach(function (link) {
      link.addEventListener('mouseenter', function () {
        var menu = link.closest('.mega-menu');
        menu.querySelectorAll('.mega-link-expand').forEach(function (b) {
          b.classList.remove('active-sub');
        });
        menu.querySelectorAll('.mega-col-2').forEach(function (c) {
          c.classList.remove('active');
        });
      });
    });

    function resetMegaPanels(menu) {
      if (!menu) {
        return;
      }
      menu.querySelectorAll('.mega-link-expand').forEach(function (b) {
        b.classList.remove('active-sub');
      });
      menu.querySelectorAll('.mega-col-2').forEach(function (c) {
        c.classList.remove('active');
      });
    }

    function closeAllMegaMenus() {
      document.querySelectorAll('.mega-menu.open').forEach(function (m) {
        m.classList.remove('open');
      });
      document.querySelectorAll('.nav-tab').forEach(function (t) {
        t.setAttribute('aria-expanded', 'false');
      });
    }

    /* Desktop mega menu: hover open/close */
    root.querySelectorAll('.nav-item').forEach(function (item) {
      var tab = item.querySelector('.nav-tab');
      var menu = item.querySelector('.mega-menu');

      if (tab && menu) {
        item.addEventListener('mouseenter', function () {
          closeAllMegaMenus();
          menu.classList.add('open');
          tab.setAttribute('aria-expanded', 'true');
        });

        item.addEventListener('mouseleave', function () {
          menu.classList.remove('open');
          tab.setAttribute('aria-expanded', 'false');
          resetMegaPanels(menu);
        });
      } else {
        item.addEventListener('mouseleave', function () {
          resetMegaPanels(item.querySelector('.mega-menu'));
        });
      }
    });

    /* Mobile hamburger panel */
    var hamburgerBtn = document.getElementById('hamburgerBtn');
    var mobilePanel = document.getElementById('mobileMenuPanel');
    var mobileRoot = document.getElementById('mobileAccordionRoot');
    var mobileDrill = document.getElementById('mobileDrilldown');

    if (hamburgerBtn && mobilePanel) {
      function closeMobilePanel() {
        mobilePanel.classList.remove('open');
        hamburgerBtn.setAttribute('aria-expanded', 'false');
        hamburgerBtn.innerHTML = '&#9776;';
      }

      hamburgerBtn.addEventListener('click', function (e) {
        e.stopPropagation();
        var isOpen = mobilePanel.classList.toggle('open');
        hamburgerBtn.setAttribute('aria-expanded', isOpen ? 'true' : 'false');
        hamburgerBtn.innerHTML = isOpen ? '&#10005;' : '&#9776;';
        if (isOpen && mobileDrill && mobileRoot) {
          mobileDrill.classList.remove('open');
          mobileRoot.style.display = 'block';
        }
      });

      mobilePanel.addEventListener('click', function (e) {
        e.stopPropagation();
      });
      document.addEventListener('click', function () {
        closeMobilePanel();
      });

      root.querySelectorAll('.m-acc-toggle').forEach(function (btn) {
        btn.addEventListener('click', function () {
          var isOpen = btn.getAttribute('aria-expanded') === 'true';
          root.querySelectorAll('.m-acc-toggle').forEach(function (b) {
            b.setAttribute('aria-expanded', 'false');
            var target = document.getElementById(b.dataset.target);
            if (target) {
              target.style.display = 'none';
            }
          });
          if (!isOpen) {
            btn.setAttribute('aria-expanded', 'true');
            var panel = document.getElementById(btn.dataset.target);
            if (panel) {
              panel.style.display = 'block';
            }
          }
        });
      });

      var drillTitle = document.getElementById('mobileDrilldownTitle');
      var drillList = document.getElementById('mobileDrilldownList');
      var mobileBackBtn = document.getElementById('mobileBackBtn');

      root.querySelectorAll('.m-drill').forEach(function (btn) {
        btn.addEventListener('click', function () {
          if (!drillTitle || !drillList || !mobileRoot || !mobileDrill) {
            return;
          }
          var key = btn.dataset.drill;
          var panel = root.querySelector('.mega-col-2[data-sub-panel="' + key + '"]');
          var items = [];
          if (panel) {
            panel.querySelectorAll('.mega-link').forEach(function (link) {
              items.push(link.textContent.trim());
            });
          }
          drillTitle.textContent = btn.textContent.replace(/\s*›\s*$/, '').trim();
          drillList.innerHTML = items.map(function (label) {
            return '<a href="#" class="m-link">' + label + '</a>';
          }).join('');
          mobileRoot.style.display = 'none';
          mobileDrill.classList.add('open');
        });
      });

      if (mobileBackBtn && mobileDrill && mobileRoot) {
        mobileBackBtn.addEventListener('click', function () {
          mobileDrill.classList.remove('open');
          mobileRoot.style.display = 'block';
        });
      }
    }

    document.addEventListener('keydown', function (e) {
      if (e.key === 'Escape') {
        document.querySelectorAll('.mega-menu.open').forEach(function (m) {
          m.classList.remove('open');
        });
        if (mobilePanel) {
          mobilePanel.classList.remove('open');
        }
        if (hamburgerBtn) {
          hamburgerBtn.setAttribute('aria-expanded', 'false');
          hamburgerBtn.innerHTML = '&#9776;';
        }
      }
    });
  }

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initNysifNavigation);
  } else {
    initNysifNavigation();
  }
})();

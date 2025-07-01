// 主题切换逻辑
(function () {
 
  const themeBtn = document.getElementById('theme-toggle');
    const setTheme = (dark) => {
    
    if (dark) {
      document.body.classList.add('dark');
      localStorage.setItem('theme', 'dark');
    } else {
      document.body.classList.remove('dark');
      localStorage.setItem('theme', 'light');
    }
  };
  // 初始化
  const userTheme = localStorage.getItem('theme');
  if (userTheme === 'dark' || (userTheme !== 'light' && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
    setTheme(true);
  } else {
    setTheme(false);
  }
  if (themeBtn) {
    themeBtn.onclick = () => setTheme(!document.body.classList.contains('dark'));
  }
})();

 
// 首页打字机动效
window.addEventListener('DOMContentLoaded', function() {
  var typewriter = document.getElementById('typewriter');
  if (typewriter) {
    var text = typewriter.getAttribute('data-text');
    var i = 0;
    function typing() {
      if (i <= text.length) {
        typewriter.textContent = text.slice(0, i);
        i++;
        setTimeout(typing, 32);
      }
    }
    typing();
  }
});

// 主题切换按钮图标自适应
function updateThemeIcon() {
    const btn = document.getElementById('theme-toggle');
    if (document.body.classList.contains('dark')) {
        btn.textContent = '☀️';
    } else {
        btn.textContent = '🌙';
    }
}
updateThemeIcon();
const observer = new MutationObserver(updateThemeIcon);
observer.observe(document.body, { attributes: true, attributeFilter: ['class'] });


   
 
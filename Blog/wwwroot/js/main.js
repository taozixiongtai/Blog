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

window.blogPosts = [
  {
    id: 1,
    title: 'Welcome to devlopr-jekyll !',
    subtitle: 'Getting Started using devlopr-jekyll',
    author: 'johndoe',
    date: 'May 22, 2019',
    category: 'jekyll',
    image: 'https://images.unsplash.com/photo-1519389950473-47ba0277781c?auto=format&fit=crop&w=900&q=80',
    content: `<p>这里是正文内容示例。你可以在 main.js 里为每篇文章配置不同的内容、图片、作者等信息。</p>`
  },
  {
    id: 2,
    title: 'Added Multi Author Support',
    subtitle: 'Support for multiple authors in your blog',
    author: 'johndoe',
    date: 'Oct 24, 2020',
    category: 'jekyll',
    image: 'https://images.unsplash.com/photo-1465101046530-73398c7f28ca?auto=format&fit=crop&w=900&q=80',
    content: `<p>多作者支持示例内容。</p>`
  },
  {
    id: 3,
    title: 'Added Latex Support',
    subtitle: 'You can now use LaTeX to write equations',
    author: 'johndoe',
    date: 'Oct 24, 2020',
    category: 'jekyll',
    image: 'https://images.unsplash.com/photo-1465101046530-73398c7f28ca?auto=format&fit=crop&w=900&q=80',
    content: `<p>LaTeX 支持示例内容。</p>`
  }
];
// 渲染文章列表
const list = document.getElementById('post-list');
window.blogPosts.forEach(post => {
    const li = document.createElement('li');
    li.className = 'article-item';
    li.innerHTML = `
    <img class="article-avatar" src="https://i.pravatar.cc/40?img=5" alt="作者">
    <div class="article-info">
        <a class="article-title-link" href="detail?id=${post.id}">${post.title}</a>
        <div class="article-meta">- ${post.date || ''}</div>
        <span class="article-tag">${post.category || ''}</span>
    </div>
              `;
    list.appendChild(li);
});
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


   
 
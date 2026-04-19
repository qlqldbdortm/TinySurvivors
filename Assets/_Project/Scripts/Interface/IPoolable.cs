namespace TinySurvivors.Interface
{
    public interface IPoolable
    {
        /// <summary>
        /// 오브젝트를 풀에서 꺼내서 사용하기 직전에 호출 (초기화)
        /// </summary>
        public void Init();
        
        /// <summary>
        /// 오브젝트를 풀에 반환하기 직전에 호출 (상태 정리 / 리셋)
        /// </summary>
        public void Release();
    }
}
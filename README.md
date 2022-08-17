# SlimeThrow


## 개발 규칙
### Unity 설정
- 유니티 버전 : 2021.3.8f1

### Unity 개발 규칙
- 네이밍 (자기 Scene에서만 할땐 상관 없음)
  - 이미지, 애니메이션 등 애셋, GameObject : PlayerHand (PascalCase) 사용, 필요시 _ 사용
  - Scene : YJ_MainScene (이니셜_Scene이름)
- 자기 Scene에서만 작업하기
  - Scene 이름 앞에 이니셜 붙이기 (YJ, JY, SH)
  - 다른 사람이 만든 Prefab은 물어보고 변경하기 or 변경 후 말해주기 or 다른 브랜치에서 작업하기
- 다른 사람의 코드나 여러 씬을 한번에 수정하는 리팩토링 할 땐 꼭 브랜치 새로 파서 하기
- 이미지 Import 시
  - Pixels Per Unit : 32
  - Filter Mode : Point
  - Compression : None
  
### C# 개발 
- 네이밍 (자기만 쓸 것 같은 코드 말곤 지키기)
  - 지역 변수 : playerSpeed (cammelCase)
  - private 변수 : _playerSpeed (언더바 붙이기)
  - 그 외 모든 것 : ThrowSlime (PascalCase)
- Inspector에 안보여도 되는 변수는 숨기기
- public, protected 변수(field)는 웬만해서 사용하지 말고, 필요할 경우 속성(property)으로 만들어서 쓰기
- Singleton은 Awake()에서 초기화되므로 Singleton 참조할 땐 Start()에서 하기